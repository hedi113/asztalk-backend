import { useEffect, useState } from "react";
import type { Auto } from "../types/Auto";
import apiClient from "../api/apiClient";
import { toast } from "react-toastify";
import { Button, Table } from "react-bootstrap";
import { FaTrash } from "react-icons/fa6";

const Cart = () => {
  const [autok, setAutok] = useState<Auto[]>([]);

  useEffect(() => {
    apiClient
      .get("/autok")
      .then((response) => setAutok(response.data))
      .catch(() => toast.error("Nem sikerült betölteni az autókat!"));
  }, []);

  const [kosar, setKosar] = useState<number[]>(
    JSON.parse(localStorage.getItem("cart") ?? "[]")
  );

  useEffect(() => {
    localStorage.setItem("cart", JSON.stringify(kosar));
  }, [kosar]);

  const removeItem = (searchIndex: number) => {
    setKosar(kosar.filter((_, index) => index != searchIndex));
    toast.success("Sikeres törlés!");
  };

  return (
    <>
      <Table striped bordered>
        <thead>
          <th>Modell</th>
          <th>Ár</th>
          <th>Törlés</th>
        </thead>
        <tbody>
          {kosar.map((id, index) => {
            const auto = autok.find((p) => p?.id == id);

            return (
              <>
                <tr>
                  <td>{auto?.modell}</td>
                  <td>{auto?.ar}</td>
                  <td>
                    <Button onClick={() => removeItem(index)} variant="danger">
                      <FaTrash />
                    </Button>
                  </td>
                </tr>
              </>
            );
          })}
        </tbody>
        <Button onClick={() => setKosar([])} variant="danger">Kiürítés</Button>
      </Table>
    </>
  );
};
export default Cart;
