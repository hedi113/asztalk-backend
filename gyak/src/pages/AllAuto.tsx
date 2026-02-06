import { useEffect, useState } from "react";
import type { Auto } from "../types/Auto";
import apiClient, { BASE_URL } from "../api/apiClient";
import { Button, Card, Carousel, Col, Container, Row } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

const AllAuto = () => {
  const navigate = useNavigate();
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

  const generateCard = (a: Auto) => {
    return (
      <Col>
        <Card style={{ width: "18rem" }}>
            <Carousel>
              {a?.images.map((url) => (
                <Carousel.Item interval={3000}>
                  <img src={`${BASE_URL}/kepek/${url}`} width={300} height={200}/>
                  
                </Carousel.Item>
              ))}
            </Carousel>
          <Card.Body>
            <Card.Title>{a.ar}</Card.Title>
            <Card.Text>{a.leiras}</Card.Text>
            <Button variant="success" onClick={() => navigate(`/auto/${a.id}`)}>
              Megtekintés
            </Button>
            <Button
              onClick={() => {
                setKosar([...kosar, Number(a.id)]);
                toast.success("Sikeresen a kosárba tetted a terméket!");
              }}
              variant="primary"
            >
              Kosárba
            </Button>
          </Card.Body>
        </Card>
      </Col>
    );
  };

  return (
    <>
      <Container>
        <Row xs={1} md={"auto"} className="g-4">
          {autok.map((a) => generateCard(a))}
        </Row>
      </Container>
    </>
  );
};

export default AllAuto;
