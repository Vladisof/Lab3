import {useEffect, useState} from "react";
import {apiEndpoint} from "../api";
import {launchError} from "../components/layout/Layout";
import {useNavigate} from "react-router-dom";

const Spectacles = () => {
    const [spectacles, setSpectacles] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        apiEndpoint('spectacle').fetch()
            .then(res => setSpectacles(res.data))
            .catch(err => launchError(err));
    }, []);

    const handleSearch = (e) => {
        e.preventDefault();

        const searchValue = e.target['searchValue'].value;
        const searchBy = e.target['searchBy'].value;

        const data = {
            searchValue,
            searchBy
        }

        apiEndpoint(`spectacle`).post(data)
            .then(res => setSpectacles(res.data))
            .catch(err => launchError(err));
    }

    const makeOrder = (paid, id) => {
        const data = {
            isPaid: paid,
            ticketId: id,
            quantity: 1
        }

        apiEndpoint(`order/make`).post(data)
            .then(res => navigate('/orders'))
            .catch(err => launchError(err));
    }

    return (
        <>
            <div className="container mt-5">
                <h2>Афіши</h2>
                <form method="post" onSubmit={handleSearch}>
                    <div className="form-group col-md-4">
                        <label htmlFor="selectedFilter">Пошук за</label>
                        <select className="form-control" id="selectedFilter" name="searchBy">
                            <option value="name">Назвою</option>
                            <option value="date">Датою</option>
                            <option value="author">Автором</option>
                            <option value="genre">Жанром</option>
                        </select>
                    </div>

                    <div className="form-group col-md-4">
                        <input type="text" className="form-control" id="searchValue" name="searchValue"
                               placeholder="Введіть значення"/>
                    </div>

                    <div className="form-group col-md-12">
                        <button type="submit" className="btn btn-primary">Пошук</button>
                    </div>
                </form>
                {
                    spectacles.map(spectacle =>
                        <div className="card mb-3">
                            <div className="card-header">
                                Назва афіши: {spectacle.name}
                            </div>
                            <div className="card-text">
                                <div className="row">
                                    <div className="col-6 ml-3">
                                        <p>Опис: {spectacle.description}</p>
                                    </div>
                                    <div className="col-5">
                                        <p>Дата: {spectacle.date}</p>
                                        <p>Жанр: {spectacle['genre']}</p>
                                        <p>Автор: {spectacle.author}</p>
                                    </div>
                                </div>
                            </div>
                            <div className="card-body">
                                <h5 className="card-title">Квитки</h5>
                                <ul className="list-group">
                                    {
                                        spectacle['tickets'].map(ticket =>
                                            <li className="list-group-item d-flex justify-content-between align-items-center">
                                                Назва: {ticket.name}
                                                <span
                                                    className="badge badge-primary badge-pill">Залишилось: {ticket.amount} квитків</span>
                                                <span
                                                    className="badge badge-success">Ціна: {ticket['price']} грн.</span>
                                                <div className="btn-group" role="group">
                                                    <button type="button" className="btn btn-primary"
                                                            onClick={() => makeOrder(true, ticket.id)}>
                                                        Купити
                                                    </button>
                                                    <button type="button" className="btn btn-secondary"
                                                            onClick={() => makeOrder(false, ticket.id)}>
                                                        Забронювати
                                                    </button>
                                                </div>
                                            </li>
                                        )
                                    }
                                </ul>
                            </div>
                        </div>
                    )
                }
            </div>
        </>
    )
}

export default Spectacles;