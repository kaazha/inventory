CREATE TABLE "categories" (
    "id" INTEGER NOT NULL CONSTRAINT "PK_categories" PRIMARY KEY AUTOINCREMENT,
    "name" TEXT NOT NULL,
    "description" TEXT NULL
);


CREATE TABLE "products" (
    "id" TEXT NOT NULL CONSTRAINT "PK_products" PRIMARY KEY,
    "product_number" TEXT NOT NULL,
    "name" TEXT NOT NULL,
    "description" TEXT NULL,
    "category_id" INTEGER NOT NULL,
    "sub_category_id" INTEGER NOT NULL,
    "model" TEXT NULL,
    "color" TEXT NULL,
    "size" TEXT NULL,
    "unit_of_measurement" TEXT NULL,
    "weight" REAL NULL,
    "style" TEXT NULL,
    "reorder_point" INTEGER NULL,
    "standard_cost" REAL NULL,
    "list_price" REAL NULL,
    "product_photo" TEXT NULL,
    "is_active" INTEGER NOT NULL,
    "modified_date" TEXT NULL,
    CONSTRAINT "FK_products_categories_category_id" FOREIGN KEY ("category_id") REFERENCES "categories" ("id"),
    CONSTRAINT "FK_products_categories_sub_category_id" FOREIGN KEY ("sub_category_id") REFERENCES "categories" ("id")
);


CREATE UNIQUE INDEX "IX_categories_name" ON "categories" ("name");


CREATE INDEX "IX_products_category_id" ON "products" ("category_id");


CREATE UNIQUE INDEX "IX_products_product_number" ON "products" ("product_number");


CREATE INDEX "IX_products_sub_category_id" ON "products" ("sub_category_id");


