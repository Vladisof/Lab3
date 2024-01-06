import {useEffect, useState} from "react";
import {apiEndpoint} from "../api";
import {launchError, launchSuccess} from "../components/layout/Layout";

const Orders = () => {
    const [orders, setOrders] = useState([]);

    useEffect(() => {
        apiEndpoint('order').fetch()
            .then(res => setOrders(res.data))
            .catch(err => launchError(err));
    }, []);

    const handleUpdate = (id) => {
        apiEndpoint('order/update').post(id)
            .then(res => {
                setOrders(res.data);
                launchSuccess({data: 'Квитки успішно придбані!'});
            })
            .catch(err => launchError(err));
    }

    return (
        <>
            <div className="container mt-5">
                <h2>Ваші замовлення</h2>
                {
                    orders.map(order => (
                        <div className="card mb-3">
                            <div className="card-header">
                                Order ID: {order.id}
                            </div>
                            <div className="card-body">
                                <h5 className="card-title">Вистава: {order['ticket']['spectacle'].name}</h5>
                                <p className="card-text">Назва квитка: {order['ticket'].name}</p>
                                <p className="card-text">Кількість квитків: {order.quantity}</p>
                                <p className="card-text">Ціна: {order['totalPrice']} грн</p>
                                {
                                    !order['isPaid'] ?
                                        <>
                                            <p className="card-text">Статус: бронь</p>
                                            <button type="submit" className="btn btn-primary"
                                                    onClick={() => handleUpdate(order.id)}>
                                                Купити
                                            </button>
                                        </>
                                        :
                                        <p className="card-text">Статус: оплачено</p>
                                }
                            </div>
                        </div>
                    ))
                }
            </div>
        </>
    )
}

export default Orders;