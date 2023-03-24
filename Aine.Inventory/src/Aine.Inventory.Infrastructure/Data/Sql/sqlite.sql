CREATE TABLE "location" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_location" PRIMARY KEY AUTOINCREMENT,
    "name" TEXT NOT NULL
);

CREATE TABLE "product_category" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_product_category" PRIMARY KEY AUTOINCREMENT,
    "name" TEXT NOT NULL,
    "description" TEXT NULL
);


CREATE TABLE "product_model" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_product_model" PRIMARY KEY AUTOINCREMENT,
    "name" TEXT NOT NULL,
    "description" TEXT NULL
);


CREATE TABLE "product_subcategory" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_product_subcategory" PRIMARY KEY AUTOINCREMENT,
    "category_id" INTEGER NOT NULL,
    "name" TEXT NOT NULL,
    "description" TEXT NULL,
    CONSTRAINT "FK_product_subcategory_product_category_category_id" FOREIGN KEY ("category_id") REFERENCES "product_category" ("id") ON DELETE CASCADE
);


CREATE TABLE "product" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_product" PRIMARY KEY AUTOINCREMENT,
    "product_number" TEXT NOT NULL,
    "name" TEXT NOT NULL,
    "description" TEXT NULL,
    "sub_category_id" INTEGER NULL,
    "model_id" INTEGER NULL,
    "color" TEXT NULL,
    "size" TEXT NULL,
    "size_unit" TEXT NULL,
    "weight" REAL NULL,
    "weight_unit" TEXT NULL,
    "style" TEXT NULL,
    "reorder_point" INTEGER NULL,
    "standard_cost" REAL NULL,
    "list_price" REAL NULL,
    "is_active" INTEGER NOT NULL,
    "modified_date" TEXT NULL,
    "created_by" TEXT NULL,
    "modified_by" TEXT NULL,
    CONSTRAINT "FK_product_product_model_model_id" FOREIGN KEY ("model_id") REFERENCES "product_model" ("id"),
    CONSTRAINT "FK_product_product_subcategory_sub_category_id" FOREIGN KEY ("sub_category_id") REFERENCES "product_subcategory" ("id")
);

CREATE TABLE "product_transaction" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_product_transaction" PRIMARY KEY AUTOINCREMENT,
    "product_id" INTEGER NOT NULL,
    "transaction_date" TEXT NOT NULL,
    "transaction_type" TEXT NOT NULL,
    "reference_number" TEXT NULL,
    "quantity" INTEGER NOT NULL,
    "total_cost" REAL NULL,
    "notes" TEXT NULL,
    "created_by" TEXT NULL,
    "date_created" TEXT NOT NULL,
    "modified_by" TEXT NULL,
    "modified_date" TEXT NULL,
    CONSTRAINT "FK_product_transaction_product_product_id" FOREIGN KEY ("product_id") REFERENCES "product" ("id")
    --CONSTRAINT "CK_product_transaction_trans_type" check "transaction_type" in ('Sales', 'Purchase', 'Inflow', 'Outflow')
);


CREATE TABLE "product_inventory" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_product_inventory" PRIMARY KEY AUTOINCREMENT,
    "location_id" INTEGER NOT NULL,
    "product_id" INTEGER NOT NULL,
    "shelf" TEXT NULL,
    "bin" TEXT NULL,
    "quantity" INTEGER NOT NULL,
    "modified_date" TEXT NULL,
    CONSTRAINT "FK_product_inventory_location_location_id" FOREIGN KEY ("location_id") REFERENCES "location" ("id") ON DELETE CASCADE,
    CONSTRAINT "FK_product_inventory_product_product_id" FOREIGN KEY ("product_id") REFERENCES "product" ("id") ON DELETE CASCADE
);


CREATE TABLE "product_price" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_product_price" PRIMARY KEY AUTOINCREMENT,
    "product_id" INTEGER NOT NULL,
    "effective_date" TEXT NOT NULL,
    "end_date" TEXT NULL,
    "date_changed" TEXT NULL,
    "changed_by" TEXT NULL,
    "list_price" REAL NOT NULL,
    "price_change" REAL NULL,
    "notes" TEXT NULL,
    CONSTRAINT "FK_product_price_product" FOREIGN KEY ("product_id") REFERENCES "product" ("id") ON DELETE CASCADE
);


CREATE TABLE "product_photo" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_product_photo" PRIMARY KEY AUTOINCREMENT,
    "product_id" INTEGER NOT NULL,    
    "thumbnail_photo_filename" TEXT NOT NULL,    
    "large_photo_filename" TEXT NOT NULL, 
    CONSTRAINT "FK_product_photo_product_product_id" FOREIGN KEY ("product_id") REFERENCES "product" ("id") ON DELETE CASCADE
);


CREATE TABLE "settings" (
    "setting_name" TEXT NOT NULL CONSTRAINT "PK_settings" PRIMARY KEY,
    "description" TEXT NULL,
    "setting_value" TEXT NOT NULL
);

insert into settings(setting_name, setting_value)
values
    ('CompanyName', 'GraceCorp'),
    ('CompanyLogo', 'companyLogo.png'),
    ('BusinessType', 'Commerce'),
    ('DateCreated', '2023-01-01'),
    ('InventoryItemName', 'Inventory Item'),
    ('MultipleLocations', 'true'),
    ('TransactionTypes', '[{"name": "Sales", "type": "outflow", "affectsQuantities": true}, {"name": "Recieve Items", "type": "inflow", "affectsQuantities": true}, {"name": "Transfer", "type": "transfer", "affectsQuantities": true}]');

CREATE UNIQUE INDEX "IX_location_name" ON "location" ("name");


CREATE INDEX "IX_product_model_id" ON "product" ("model_id");


CREATE UNIQUE INDEX "IX_product_product_number" ON "product" ("product_number");


CREATE INDEX "IX_product_sub_category_id" ON "product" ("sub_category_id");


CREATE UNIQUE INDEX "IX_product_category_name" ON "product_category" ("name");


CREATE INDEX "IX_product_inventory_location_id" ON "product_inventory" ("location_id");


CREATE INDEX "IX_product_inventory_product_id" ON "product_inventory" ("product_id");


CREATE UNIQUE INDEX "IX_product_model_name" ON "product_model" ("name");


CREATE UNIQUE INDEX "IX_product_photo_product_id" ON "product_photo" ("product_id");


CREATE INDEX "IX_product_subcategory_category_id" ON "product_subcategory" ("category_id");


CREATE UNIQUE INDEX "IX_product_subcategory_name" ON "product_subcategory" ("name");

CREATE UNIQUE INDEX "IX_product_price_unique" ON "product_price" ("product_id", "effective_date", "end_date");


/******************************************** SECURITY ********************************************************************/

CREATE TABLE "auth_tokens" (
    "user_id" TEXT NOT NULL CONSTRAINT "PK_auth_tokens" PRIMARY KEY,
    "expiry_date" TEXT NOT NULL,
    "token" TEXT NOT NULL
);

CREATE TABLE "roles" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_roles" PRIMARY KEY AUTOINCREMENT,
    "role_name" TEXT NOT NULL,
    "role_description" TEXT NULL
);

insert into roles(role_name, role_description) values
  ('Admin', 'System Administrator'),
  ('Manager', 'Manager'),
  ('User', 'Standard User');


CREATE TABLE "permissions" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_permissions" PRIMARY KEY AUTOINCREMENT,
    "permission_title" TEXT NOT NULL,
    "permission_description" TEXT NULL
);

insert into permissions(permission_title, permission_description) values
  ('Manage Products', 'create and update products/stock'),
  ('Manage Transactions', 'Enter transactions'),
  ('Manage Product Price', 'Update product price history'),
  ('Manage Inventory', 'Create and update product inventory/quantities'),
  ('Manage Users', 'Create and assign permissions to users');

CREATE TABLE "users" (
    "user_id" INTEGER NOT NULL CONSTRAINT "PK_users" PRIMARY KEY AUTOINCREMENT,
    "user_name" TEXT NOT NULL,
    "full_name" TEXT NOT NULL,
    "avatar" BLOB NULL,
    "password" TEXT NOT NULL,
    "date_created" TEXT NOT NULL,
    "created_by" TEXT NULL,
    "is_active" INTEGER NOT NULL,
    "last_updated" TEXT NULL,
    "last_updated_by" TEXT NULL,
    "last_login" TEXT NULL
);

CREATE TABLE "user_permissions" (
    "permission_id" INTEGER NOT NULL,
    "user_id" INTEGER NOT NULL,
    "permission_flag" INTEGER NOT NULL, /*0:deny, */
    CONSTRAINT "PK_user_permissions" PRIMARY KEY ("user_id", "permission_id"),
    CONSTRAINT "FK_user_permissions_permissions_permissions" FOREIGN KEY ("permission_id") REFERENCES "permissions" ("Id") ,
    CONSTRAINT "FK_user_permissions_users_user_id" FOREIGN KEY ("user_id") REFERENCES "users" ("user_id") ON DELETE CASCADE
);

CREATE TABLE "user_roles" (
    "role_id" INTEGER NOT NULL,
    "user_id" INTEGER NOT NULL,
    CONSTRAINT "PK_user_roles" PRIMARY KEY ("user_id", "role_id"),
    CONSTRAINT "FK_user_roles_roles_roles" FOREIGN KEY ("role_id") REFERENCES "roles" ("Id") ,
    CONSTRAINT "FK_user_roles_users_user_id" FOREIGN KEY ("user_id") REFERENCES "users" ("user_id") ON DELETE CASCADE
);

CREATE UNIQUE INDEX "IX_roles_role_name" ON "roles" ("role_name");
CREATE UNIQUE INDEX "IX_permissions_permission_title" ON "permissions" ("permission_title");
CREATE UNIQUE INDEX "IX_users_user_name" ON "users" ("user_name");

/******************************************** SECURITY ********************************************************************/

