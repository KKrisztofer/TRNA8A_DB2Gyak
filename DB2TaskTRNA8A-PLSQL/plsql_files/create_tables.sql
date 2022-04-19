CREATE TABLE OrderItems
(
  order_id NUMBER NOT NULL,
  product_id NUMBER NOT NULL,
  FOREIGN KEY (order_id) REFERENCES Orders(id),
  FOREIGN KEY (product_id) REFERENCES Products(id)
);