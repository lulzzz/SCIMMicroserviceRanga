using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace ScimMicroservice.Models
{
    public class ResourceExtensions : IEnumerable<KeyValuePair<string, ScimResourceExtension>>
    {
        private readonly ConcurrentDictionary<string, KeyValuePair<string, ScimResourceExtension>> _Extensions;

        public ResourceExtensions()
        {
            _Extensions = new ConcurrentDictionary<string, KeyValuePair<string, ScimResourceExtension>>();
        }

        public ISet<string> Schemas
        {
            get
            {
                return new HashSet<string>(_Extensions.Values.Select(kvp => kvp.Key));
            }
        }

        public bool Contains(Type extensionType)
        {
            return _Extensions.ContainsKey(extensionType.FullName);
        }

        public T GetOrCreate<T>() where T : ScimResourceExtension, new()
        {
            return _Extensions.GetOrAdd(
                typeof(T).FullName,
                type =>
                {
                    var instance = (ScimResourceExtension)default(T);//.CreateInstance();
                    return new KeyValuePair<string, ScimResourceExtension>(instance.SchemaIdentifier, instance);
                }).Value as T;
        }

        public void Remove(Type extensionType)
        {
            KeyValuePair<string, ScimResourceExtension> ext;
            _Extensions.TryRemove(extensionType.FullName, out ext);
        }

        internal void Add(ScimResourceExtension extensionInstance)
        {
            if (extensionInstance == null)
                throw new ArgumentNullException(@"You cannot pass in a null value to this method. See the additional Add() overloads for adding null extensions.");

            _Extensions.AddOrUpdate(
                extensionInstance.GetType().FullName,
                new KeyValuePair<string, ScimResourceExtension>(extensionInstance.SchemaIdentifier, extensionInstance),
                (key, existing) => new KeyValuePair<string, ScimResourceExtension>(extensionInstance.SchemaIdentifier, extensionInstance));
        }

        internal void Add(Type extensionType, string extensionSchema, object value)
        {
            _Extensions.AddOrUpdate(
                extensionType.FullName,
                new KeyValuePair<string, ScimResourceExtension>(extensionSchema, null),
                (key, existing) => new KeyValuePair<string, ScimResourceExtension>(extensionSchema, null));
        }

        //internal object GetOrCreate(Type extensionType)
        //{
        //    if (!typeof(ResourceExtension).IsAssignableFrom(extensionType))
        //        throw new ArgumentException("Extension must be a type assignable from ResourceExtension.", "extensionType");

        //    return _Extensions.GetOrAdd(
        //        extensionType.FullName,
        //        type =>
        //        {
        //            var instance = (ResourceExtension)extensionType.CreateInstance();
        //            return new KeyValuePair<string, ResourceExtension>(instance.SchemaIdentifier, instance);
        //        }).Value;
        //}

        public int CalculateVersion()
        {
            var hash = 0;
            foreach (var extension in _Extensions.Values.Where(ext => ext.Value != null))
            {
                hash = hash * 31 + extension.Value.CalculateVersion();
            }

            return hash;
        }

        public IEnumerator<KeyValuePair<string, ScimResourceExtension>> GetEnumerator()
        {
            return _Extensions.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        internal IDictionary<string, JToken> ToJsonDictionary()
        {
            return _Extensions
                .Values
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value == null
                        ? (JToken)null
                        : (JToken)JObject.FromObject(kvp.Value));
        }
    }
}
