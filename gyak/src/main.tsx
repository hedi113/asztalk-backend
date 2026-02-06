import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import AllAuto from "./pages/AllAuto";
import "bootstrap/dist/css/bootstrap.min.css";
import { ToastContainer } from "react-toastify";
import Cart from "./pages/Cart";
createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<AllAuto />} />
        <Route path="/cart" element={<Cart />} />
      </Routes>
    </BrowserRouter>
    <ToastContainer />
  </StrictMode>
);
