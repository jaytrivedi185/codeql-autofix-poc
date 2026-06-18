const express = require("express");
const sqlite3 = require("sqlite3").verbose();

const app = express();
const db = new sqlite3.Database(":memory:");

app.get("/user", (req, res) => {
  const name = req.query.name;

  // Intentionally vulnerable for POC
  const query = "SELECT * FROM users WHERE name = '" + name + "'";

  db.all(query, [], (err, rows) => {
    if (err) {
      res.status(500).send("Database error");
      return;
    }

    res.json(rows);
  });
});

app.listen(3000, () => {
  console.log("Server running on port 3000");
});
