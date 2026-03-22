CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260321100834_InitialCreate') THEN
    CREATE TABLE "Categories" (
        "Id" uuid NOT NULL,
        "Name" character varying(200) NOT NULL,
        "Description" text,
        "CreatedAtUtc" timestamp with time zone NOT NULL,
        "UpdatedAtUtc" timestamp with time zone,
        CONSTRAINT "PK_Categories" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260321100834_InitialCreate') THEN
    CREATE TABLE "SalesOrders" (
        "Id" uuid NOT NULL,
        "Code" character varying(100) NOT NULL,
        "OrderDate" timestamp with time zone NOT NULL,
        "Status" integer NOT NULL,
        "SubTotalAmount" numeric(18,2) NOT NULL,
        "DiscountAmount" numeric(18,2) NOT NULL,
        "TotalAmount" numeric(18,2) NOT NULL,
        "CreatedAtUtc" timestamp with time zone NOT NULL,
        "UpdatedAtUtc" timestamp with time zone,
        CONSTRAINT "PK_SalesOrders" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260321100834_InitialCreate') THEN
    CREATE TABLE "Suppliers" (
        "Id" uuid NOT NULL,
        "Name" character varying(200) NOT NULL,
        "Phone" character varying(20),
        "Address" text,
        "TaxCode" character varying(50),
        "CreatedAtUtc" timestamp with time zone NOT NULL,
        "UpdatedAtUtc" timestamp with time zone,
        CONSTRAINT "PK_Suppliers" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260321100834_InitialCreate') THEN
    CREATE TABLE "Warehouses" (
        "Id" uuid NOT NULL,
        "Code" character varying(50) NOT NULL,
        "Name" character varying(200) NOT NULL,
        "Address" text,
        "CreatedAtUtc" timestamp with time zone NOT NULL,
        "UpdatedAtUtc" timestamp with time zone,
        CONSTRAINT "PK_Warehouses" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260321100834_InitialCreate') THEN
    CREATE TABLE "Products" (
        "Id" uuid NOT NULL,
        "Sku" character varying(100) NOT NULL,
        "Name" character varying(250) NOT NULL,
        "Unit" character varying(30),
        "CategoryId" uuid NOT NULL,
        "DefaultCostPrice" numeric(18,2) NOT NULL,
        "DefaultSellPrice" numeric(18,2) NOT NULL,
        "IsActive" boolean NOT NULL,
        "CreatedAtUtc" timestamp with time zone NOT NULL,
        "UpdatedAtUtc" timestamp with time zone,
        CONSTRAINT "PK_Products" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Products_Categories_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES "Categories" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260321100834_InitialCreate') THEN
    CREATE TABLE "PurchaseOrders" (
        "Id" uuid NOT NULL,
        "Code" character varying(100) NOT NULL,
        "OrderDate" timestamp with time zone NOT NULL,
        "Status" integer NOT NULL,
        "SupplierId" uuid NOT NULL,
        "SubTotalAmount" numeric(18,2) NOT NULL,
        "DiscountAmount" numeric(18,2) NOT NULL,
        "TotalAmount" numeric(18,2) NOT NULL,
        "CreatedAtUtc" timestamp with time zone NOT NULL,
        "UpdatedAtUtc" timestamp with time zone,
        CONSTRAINT "PK_PurchaseOrders" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_PurchaseOrders_Suppliers_SupplierId" FOREIGN KEY ("SupplierId") REFERENCES "Suppliers" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260321100834_InitialCreate') THEN
    CREATE TABLE "InventoryTransactions" (
        "Id" uuid NOT NULL,
        "ProductId" uuid NOT NULL,
        "WarehouseId" uuid NOT NULL,
        "Type" integer NOT NULL,
        "Quantity" numeric(18,2) NOT NULL,
        "UnitCost" numeric(18,2) NOT NULL,
        "TransactionDate" timestamp with time zone NOT NULL,
        "ReferenceType" character varying(30) NOT NULL,
        "ReferenceId" uuid,
        "Note" text,
        "CreatedAtUtc" timestamp with time zone NOT NULL,
        "UpdatedAtUtc" timestamp with time zone,
        CONSTRAINT "PK_InventoryTransactions" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_InventoryTransactions_Products_ProductId" FOREIGN KEY ("ProductId") REFERENCES "Products" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_InventoryTransactions_Warehouses_WarehouseId" FOREIGN KEY ("WarehouseId") REFERENCES "Warehouses" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260321100834_InitialCreate') THEN
    CREATE TABLE "SalesOrderItems" (
        "Id" uuid NOT NULL,
        "SalesOrderId" uuid NOT NULL,
        "ProductId" uuid NOT NULL,
        "Quantity" numeric(18,2) NOT NULL,
        "UnitPrice" numeric(18,2) NOT NULL,
        "LineTotal" numeric(18,2) NOT NULL,
        "CreatedAtUtc" timestamp with time zone NOT NULL,
        "UpdatedAtUtc" timestamp with time zone,
        CONSTRAINT "PK_SalesOrderItems" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_SalesOrderItems_Products_ProductId" FOREIGN KEY ("ProductId") REFERENCES "Products" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_SalesOrderItems_SalesOrders_SalesOrderId" FOREIGN KEY ("SalesOrderId") REFERENCES "SalesOrders" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260321100834_InitialCreate') THEN
    CREATE TABLE "Payments" (
        "Id" uuid NOT NULL,
        "PurchaseOrderId" uuid NOT NULL,
        "PaidAt" timestamp with time zone NOT NULL,
        "Amount" numeric(18,2) NOT NULL,
        "Method" integer NOT NULL,
        "Note" text,
        "CreatedAtUtc" timestamp with time zone NOT NULL,
        "UpdatedAtUtc" timestamp with time zone,
        CONSTRAINT "PK_Payments" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Payments_PurchaseOrders_PurchaseOrderId" FOREIGN KEY ("PurchaseOrderId") REFERENCES "PurchaseOrders" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260321100834_InitialCreate') THEN
    CREATE TABLE "PurchaseOrderItems" (
        "Id" uuid NOT NULL,
        "PurchaseOrderId" uuid NOT NULL,
        "ProductId" uuid NOT NULL,
        "Quantity" numeric(18,2) NOT NULL,
        "UnitCost" numeric(18,2) NOT NULL,
        "LineTotal" numeric(18,2) NOT NULL,
        "CreatedAtUtc" timestamp with time zone NOT NULL,
        "UpdatedAtUtc" timestamp with time zone,
        CONSTRAINT "PK_PurchaseOrderItems" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_PurchaseOrderItems_Products_ProductId" FOREIGN KEY ("ProductId") REFERENCES "Products" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_PurchaseOrderItems_PurchaseOrders_PurchaseOrderId" FOREIGN KEY ("PurchaseOrderId") REFERENCES "PurchaseOrders" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260321100834_InitialCreate') THEN
    CREATE INDEX "IX_InventoryTransactions_ProductId_WarehouseId_TransactionDate" ON "InventoryTransactions" ("ProductId", "WarehouseId", "TransactionDate");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260321100834_InitialCreate') THEN
    CREATE INDEX "IX_InventoryTransactions_WarehouseId" ON "InventoryTransactions" ("WarehouseId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260321100834_InitialCreate') THEN
    CREATE INDEX "IX_Payments_PaidAt" ON "Payments" ("PaidAt");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260321100834_InitialCreate') THEN
    CREATE INDEX "IX_Payments_PurchaseOrderId" ON "Payments" ("PurchaseOrderId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260321100834_InitialCreate') THEN
    CREATE INDEX "IX_Products_CategoryId" ON "Products" ("CategoryId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260321100834_InitialCreate') THEN
    CREATE UNIQUE INDEX "IX_Products_Sku" ON "Products" ("Sku");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260321100834_InitialCreate') THEN
    CREATE INDEX "IX_PurchaseOrderItems_ProductId" ON "PurchaseOrderItems" ("ProductId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260321100834_InitialCreate') THEN
    CREATE INDEX "IX_PurchaseOrderItems_PurchaseOrderId" ON "PurchaseOrderItems" ("PurchaseOrderId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260321100834_InitialCreate') THEN
    CREATE UNIQUE INDEX "IX_PurchaseOrders_Code" ON "PurchaseOrders" ("Code");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260321100834_InitialCreate') THEN
    CREATE INDEX "IX_PurchaseOrders_OrderDate" ON "PurchaseOrders" ("OrderDate");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260321100834_InitialCreate') THEN
    CREATE INDEX "IX_PurchaseOrders_SupplierId" ON "PurchaseOrders" ("SupplierId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260321100834_InitialCreate') THEN
    CREATE INDEX "IX_SalesOrderItems_ProductId" ON "SalesOrderItems" ("ProductId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260321100834_InitialCreate') THEN
    CREATE INDEX "IX_SalesOrderItems_SalesOrderId" ON "SalesOrderItems" ("SalesOrderId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260321100834_InitialCreate') THEN
    CREATE UNIQUE INDEX "IX_SalesOrders_Code" ON "SalesOrders" ("Code");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260321100834_InitialCreate') THEN
    CREATE INDEX "IX_SalesOrders_OrderDate" ON "SalesOrders" ("OrderDate");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260321100834_InitialCreate') THEN
    CREATE UNIQUE INDEX "IX_Warehouses_Code" ON "Warehouses" ("Code");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260321100834_InitialCreate') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260321100834_InitialCreate', '8.0.11');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260322075008_RemovePaymentMethodAddReports') THEN
    ALTER TABLE "Products" DROP CONSTRAINT "FK_Products_Categories_CategoryId";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260322075008_RemovePaymentMethodAddReports') THEN
    ALTER TABLE "Products" DROP COLUMN "IsActive";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260322075008_RemovePaymentMethodAddReports') THEN
    ALTER TABLE "Payments" DROP COLUMN "Method";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260322075008_RemovePaymentMethodAddReports') THEN
    ALTER TABLE "Products" RENAME COLUMN "Sku" TO "ModelNormalized";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260322075008_RemovePaymentMethodAddReports') THEN
    ALTER TABLE "Products" RENAME COLUMN "DefaultSellPrice" TO "StockQuantity";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260322075008_RemovePaymentMethodAddReports') THEN
    ALTER TABLE "Products" RENAME COLUMN "DefaultCostPrice" TO "ImportPrice";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260322075008_RemovePaymentMethodAddReports') THEN
    ALTER INDEX "IX_Products_Sku" RENAME TO "IX_Products_ModelNormalized";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260322075008_RemovePaymentMethodAddReports') THEN
    ALTER TABLE "Products" ADD "BrandId" uuid NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260322075008_RemovePaymentMethodAddReports') THEN
    ALTER TABLE "Products" ADD "Model" character varying(100) NOT NULL DEFAULT '';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260322075008_RemovePaymentMethodAddReports') THEN
    CREATE TABLE "Brands" (
        "Id" uuid NOT NULL,
        "Name" character varying(200) NOT NULL,
        "CreatedAtUtc" timestamp with time zone NOT NULL,
        "UpdatedAtUtc" timestamp with time zone,
        CONSTRAINT "PK_Brands" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260322075008_RemovePaymentMethodAddReports') THEN
    CREATE INDEX "IX_Products_BrandId" ON "Products" ("BrandId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260322075008_RemovePaymentMethodAddReports') THEN
    CREATE UNIQUE INDEX "IX_Brands_Name" ON "Brands" ("Name");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260322075008_RemovePaymentMethodAddReports') THEN
    ALTER TABLE "Products" ADD CONSTRAINT "FK_Products_Brands_BrandId" FOREIGN KEY ("BrandId") REFERENCES "Brands" ("Id") ON DELETE RESTRICT;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260322075008_RemovePaymentMethodAddReports') THEN
    ALTER TABLE "Products" ADD CONSTRAINT "FK_Products_Categories_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES "Categories" ("Id") ON DELETE RESTRICT;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260322075008_RemovePaymentMethodAddReports') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260322075008_RemovePaymentMethodAddReports', '8.0.11');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260322151523_RemovePaymentAndSalesPrice') THEN
    DROP TABLE "Payments";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260322151523_RemovePaymentAndSalesPrice') THEN
    ALTER TABLE "SalesOrders" DROP COLUMN "DiscountAmount";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260322151523_RemovePaymentAndSalesPrice') THEN
    ALTER TABLE "SalesOrders" DROP COLUMN "SubTotalAmount";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260322151523_RemovePaymentAndSalesPrice') THEN
    ALTER TABLE "SalesOrders" DROP COLUMN "TotalAmount";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260322151523_RemovePaymentAndSalesPrice') THEN
    ALTER TABLE "SalesOrderItems" DROP COLUMN "LineTotal";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260322151523_RemovePaymentAndSalesPrice') THEN
    ALTER TABLE "SalesOrderItems" DROP COLUMN "UnitPrice";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260322151523_RemovePaymentAndSalesPrice') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260322151523_RemovePaymentAndSalesPrice', '8.0.11');
    END IF;
END $EF$;
COMMIT;

