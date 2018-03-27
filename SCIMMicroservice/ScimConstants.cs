﻿namespace ScimMicroservice
{
    public class ScimConstants
    {
        public static class Schemas
        {
            public const string User = @"urn:ietf:params:scim:schemas:core:2.0:User";

            public const string UserEnterprise = @"urn:ietf:params:scim:schemas:extension:enterprise:2.0:User";

            public const string Group = @"urn:ietf:params:scim:schemas:core:2.0:Group";

            public const string ServiceProviderConfig = @"urn:ietf:params:scim:schemas:core:2.0:ServiceProviderConfig";

            public const string ResourceType = @"urn:ietf:params:scim:schemas:core:2.0:ResourceType";

            public const string Schema = @"urn:ietf:params:scim:schemas:core:2.0:Schema";
        }

        public static class Messages
        {
            public const string BulkRequest = @"urn:ietf:params:scim:api:messages:2.0:BulkRequest";

            public const string BulkResponse = @"urn:ietf:params:scim:api:messages:2.0:BulkResponse";

            public const string Error = @"urn:ietf:params:scim:api:messages:2.0:Error";

            public const string PatchOp = @"urn:ietf:params:scim:api:messages:2.0:PatchOp";

            public const string ListResponse = @"urn:ietf:params:scim:api:messages:2.0:ListResponse";

            public const string SearchRequest = @"urn:ietf:params:scim:api:messages:2.0:SearchRequest";
        }

        public static class Defaults
        {
            public const string URNPrefix = @"urn:";

            public const int BulkMaxOperations = 1000;

            public const int BulkMaxPayload = 1048576;

            public const int FilterMaxResults = 200;
        }

        public static class Endpoints
        {
            public const string Users = @"/users";

            public const string Groups = @"/groups";

            public const string ResourceTypes = @"/resourcetypes";

            public const string ServiceProviderConfig = @"/serviceproviderconfig";

            public const string Schemas = @"/schemas";
        }
    }
}
