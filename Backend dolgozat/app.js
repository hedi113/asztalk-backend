import * as db from "./data/db.js";
import express from "express";

const PORT = 3000;
const app = express();

app.use(express.json());

app.get("/books", (req, res) => {
  const books = db.GetAllBooks();
  return res.status(200).json(books);
});

app.get("/books/:id", (req, res) => {
  const id = +req.params.id;
  const book = db.GetBookById(id);
  if (!book) {
    return res.status(404).json({ message: "Book not found!" });
  }
  return res.status(200).json(book);
});

app.post("/books", (req, res) => {
  const { title, author, year } = req.body;

  if (!title || !author || !year) {
    return res.status(400).json({ message: "Invalid credentials!" });
  }
  db.PostBook(title, author, year);

  return res.status(201).json({ title, author, year });
});

app.listen(PORT, () => {
  console.log("fut a szerver");
});
