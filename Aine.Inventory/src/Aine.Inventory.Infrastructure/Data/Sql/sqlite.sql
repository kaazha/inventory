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
    CONSTRAINT "FK_products_categories_category_id" FOREIGN KEY ("category_id") REFERENCES "categories" ("id") --ON DELETE CASCADE
);


CREATE INDEX "IX_products_category_id" ON "products" ("category_id");


