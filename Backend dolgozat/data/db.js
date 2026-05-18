import Database from "better-sqlite3";

const db = new Database("./data/database.sqlite");

db.prepare("CREATE TABLE books(id INTEGER PRIMARY KEY AUTOINCREMENT, title STRING, author STRING, year INTEGER)").run();

export const GetAllBooks = () => db.prepare("SELECT * FROM books").all();

export const GetBookById = (id) => db.prepare("SELECT * FROM books WHERE id = ?").get(id);

export const PostBook = (title, author, year) => db.prepare("INSERT INTO books (title, author, year) VALUES (?,?,?)").run(title, author, year);

