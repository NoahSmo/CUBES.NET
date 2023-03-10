CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Carts" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "UserId" integer NOT NULL,
    CONSTRAINT "PK_Carts" PRIMARY KEY ("Id")
);

CREATE TABLE "Categories" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Name" text NOT NULL,
    "Description" text NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "UpdatedAt" timestamp with time zone NULL,
    "DeletedAt" timestamp with time zone NULL,
    CONSTRAINT "PK_Categories" PRIMARY KEY ("Id")
);

CREATE TABLE "Domains" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Name" text NOT NULL,
    "Description" text NOT NULL,
    "Email" text NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "UpdatedAt" timestamp with time zone NULL,
    "DeletedAt" timestamp with time zone NULL,
    CONSTRAINT "PK_Domains" PRIMARY KEY ("Id")
);

CREATE TABLE "Permissions" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Name" text NOT NULL,
    "Description" text NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "UpdatedAt" timestamp with time zone NULL,
    "DeletedAt" timestamp with time zone NULL,
    CONSTRAINT "PK_Permissions" PRIMARY KEY ("Id")
);

CREATE TABLE "Providers" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Name" text NOT NULL,
    "Email" text NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "UpdatedAt" timestamp with time zone NULL,
    "DeletedAt" timestamp with time zone NULL,
    CONSTRAINT "PK_Providers" PRIMARY KEY ("Id")
);

CREATE TABLE "Roles" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Name" text NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "UpdatedAt" timestamp with time zone NULL,
    "DeletedAt" timestamp with time zone NULL,
    CONSTRAINT "PK_Roles" PRIMARY KEY ("Id")
);

CREATE TABLE "Statuses" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Message" text NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "UpdatedAt" timestamp with time zone NULL,
    "DeletedAt" timestamp with time zone NULL,
    CONSTRAINT "PK_Statuses" PRIMARY KEY ("Id")
);

CREATE TABLE "Articles" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Name" text NOT NULL,
    "Description" text NOT NULL,
    "Year" integer NOT NULL,
    "Price" double precision NOT NULL,
    "Alcohol" double precision NOT NULL,
    "Stock" integer NOT NULL,
    "DomainId" integer NOT NULL,
    "CategoryId" integer NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "UpdatedAt" timestamp with time zone NULL,
    "DeletedAt" timestamp with time zone NULL,
    CONSTRAINT "PK_Articles" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Articles_Categories_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES "Categories" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Articles_Domains_DomainId" FOREIGN KEY ("DomainId") REFERENCES "Domains" ("Id") ON DELETE CASCADE
);

CREATE TABLE "DomainProvider" (
    "DomainsId" integer NOT NULL,
    "ProvidersId" integer NOT NULL,
    CONSTRAINT "PK_DomainProvider" PRIMARY KEY ("DomainsId", "ProvidersId"),
    CONSTRAINT "FK_DomainProvider_Domains_DomainsId" FOREIGN KEY ("DomainsId") REFERENCES "Domains" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_DomainProvider_Providers_ProvidersId" FOREIGN KEY ("ProvidersId") REFERENCES "Providers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "PermissionRole" (
    "PermissionsId" integer NOT NULL,
    "RolesId" integer NOT NULL,
    CONSTRAINT "PK_PermissionRole" PRIMARY KEY ("PermissionsId", "RolesId"),
    CONSTRAINT "FK_PermissionRole_Permissions_PermissionsId" FOREIGN KEY ("PermissionsId") REFERENCES "Permissions" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_PermissionRole_Roles_RolesId" FOREIGN KEY ("RolesId") REFERENCES "Roles" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Users" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Name" text NOT NULL,
    "Surname" text NOT NULL,
    "Email" text NOT NULL,
    "Phone" integer NULL,
    "RoleId" integer NOT NULL,
    "CartId" integer NULL,
    "Password" text NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "UpdatedAt" timestamp with time zone NULL,
    "DeletedAt" timestamp with time zone NULL,
    CONSTRAINT "PK_Users" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Users_Carts_CartId" FOREIGN KEY ("CartId") REFERENCES "Carts" ("Id"),
    CONSTRAINT "FK_Users_Roles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "Roles" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ProviderOrders" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Date" timestamp with time zone NOT NULL,
    "ProviderId" integer NOT NULL,
    "StatusId" integer NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "UpdatedAt" timestamp with time zone NULL,
    "DeletedAt" timestamp with time zone NULL,
    CONSTRAINT "PK_ProviderOrders" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_ProviderOrders_Providers_ProviderId" FOREIGN KEY ("ProviderId") REFERENCES "Providers" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_ProviderOrders_Statuses_StatusId" FOREIGN KEY ("StatusId") REFERENCES "Statuses" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ArticleProvider" (
    "ArticlesId" integer NOT NULL,
    "ProvidersId" integer NOT NULL,
    CONSTRAINT "PK_ArticleProvider" PRIMARY KEY ("ArticlesId", "ProvidersId"),
    CONSTRAINT "FK_ArticleProvider_Articles_ArticlesId" FOREIGN KEY ("ArticlesId") REFERENCES "Articles" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_ArticleProvider_Providers_ProvidersId" FOREIGN KEY ("ProvidersId") REFERENCES "Providers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "CartItem" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Quantity" integer NOT NULL,
    "ArticleId" integer NOT NULL,
    "CartId" integer NOT NULL,
    CONSTRAINT "PK_CartItem" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_CartItem_Articles_ArticleId" FOREIGN KEY ("ArticleId") REFERENCES "Articles" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_CartItem_Carts_CartId" FOREIGN KEY ("CartId") REFERENCES "Carts" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Images" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Url" text NOT NULL,
    "ArticleId" integer NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "UpdatedAt" timestamp with time zone NULL,
    "DeletedAt" timestamp with time zone NULL,
    CONSTRAINT "PK_Images" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Images_Articles_ArticleId" FOREIGN KEY ("ArticleId") REFERENCES "Articles" ("Id")
);

CREATE TABLE "Addresses" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Street" text NOT NULL,
    "City" text NOT NULL,
    "Country" text NOT NULL,
    "ZipCode" integer NOT NULL,
    "UserId" integer NULL,
    "DomainId" integer NULL,
    "ProviderId" integer NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "UpdatedAt" timestamp with time zone NULL,
    "DeletedAt" timestamp with time zone NULL,
    CONSTRAINT "PK_Addresses" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Addresses_Domains_DomainId" FOREIGN KEY ("DomainId") REFERENCES "Domains" ("Id"),
    CONSTRAINT "FK_Addresses_Providers_ProviderId" FOREIGN KEY ("ProviderId") REFERENCES "Providers" ("Id"),
    CONSTRAINT "FK_Addresses_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id")
);

CREATE TABLE "Comments" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Rating" integer NOT NULL,
    "Message" text NOT NULL,
    "ArticleId" integer NOT NULL,
    "UserId" integer NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "UpdatedAt" timestamp with time zone NULL,
    "DeletedAt" timestamp with time zone NULL,
    CONSTRAINT "PK_Comments" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Comments_Articles_ArticleId" FOREIGN KEY ("ArticleId") REFERENCES "Articles" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Comments_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Orders" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Date" timestamp with time zone NOT NULL,
    "UserId" integer NOT NULL,
    "AddressId" integer NOT NULL,
    "StatusId" integer NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "UpdatedAt" timestamp with time zone NULL,
    "DeletedAt" timestamp with time zone NULL,
    CONSTRAINT "PK_Orders" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Orders_Addresses_AddressId" FOREIGN KEY ("AddressId") REFERENCES "Addresses" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Orders_Statuses_StatusId" FOREIGN KEY ("StatusId") REFERENCES "Statuses" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Orders_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ArticleOrder" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "ArticleId" integer NOT NULL,
    "OrderId" integer NOT NULL,
    "Quantity" integer NOT NULL,
    "ProviderOrderId" integer NULL,
    CONSTRAINT "PK_ArticleOrder" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_ArticleOrder_Articles_ArticleId" FOREIGN KEY ("ArticleId") REFERENCES "Articles" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_ArticleOrder_Orders_OrderId" FOREIGN KEY ("OrderId") REFERENCES "Orders" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_ArticleOrder_ProviderOrders_ProviderOrderId" FOREIGN KEY ("ProviderOrderId") REFERENCES "ProviderOrders" ("Id")
);

CREATE INDEX "IX_Addresses_DomainId" ON "Addresses" ("DomainId");

CREATE INDEX "IX_Addresses_ProviderId" ON "Addresses" ("ProviderId");

CREATE INDEX "IX_Addresses_UserId" ON "Addresses" ("UserId");

CREATE INDEX "IX_ArticleOrder_ArticleId" ON "ArticleOrder" ("ArticleId");

CREATE INDEX "IX_ArticleOrder_OrderId" ON "ArticleOrder" ("OrderId");

CREATE INDEX "IX_ArticleOrder_ProviderOrderId" ON "ArticleOrder" ("ProviderOrderId");

CREATE INDEX "IX_ArticleProvider_ProvidersId" ON "ArticleProvider" ("ProvidersId");

CREATE INDEX "IX_Articles_CategoryId" ON "Articles" ("CategoryId");

CREATE INDEX "IX_Articles_DomainId" ON "Articles" ("DomainId");

CREATE INDEX "IX_CartItem_ArticleId" ON "CartItem" ("ArticleId");

CREATE INDEX "IX_CartItem_CartId" ON "CartItem" ("CartId");

CREATE UNIQUE INDEX "IX_Categories_Name" ON "Categories" ("Name");

CREATE INDEX "IX_Comments_ArticleId" ON "Comments" ("ArticleId");

CREATE INDEX "IX_Comments_UserId" ON "Comments" ("UserId");

CREATE INDEX "IX_DomainProvider_ProvidersId" ON "DomainProvider" ("ProvidersId");

CREATE UNIQUE INDEX "IX_Domains_Name" ON "Domains" ("Name");

CREATE INDEX "IX_Images_ArticleId" ON "Images" ("ArticleId");

CREATE INDEX "IX_Orders_AddressId" ON "Orders" ("AddressId");

CREATE INDEX "IX_Orders_StatusId" ON "Orders" ("StatusId");

CREATE INDEX "IX_Orders_UserId" ON "Orders" ("UserId");

CREATE INDEX "IX_PermissionRole_RolesId" ON "PermissionRole" ("RolesId");

CREATE INDEX "IX_ProviderOrders_ProviderId" ON "ProviderOrders" ("ProviderId");

CREATE INDEX "IX_ProviderOrders_StatusId" ON "ProviderOrders" ("StatusId");

CREATE UNIQUE INDEX "IX_Providers_Email" ON "Providers" ("Email");

CREATE UNIQUE INDEX "IX_Statuses_Message" ON "Statuses" ("Message");

CREATE UNIQUE INDEX "IX_Users_CartId" ON "Users" ("CartId");

CREATE UNIQUE INDEX "IX_Users_Email" ON "Users" ("Email");

CREATE INDEX "IX_Users_RoleId" ON "Users" ("RoleId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230202095523_Initial', '7.0.1');

COMMIT;

