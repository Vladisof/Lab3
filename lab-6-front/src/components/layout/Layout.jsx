import {ToastContainer, toast} from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import {Outlet} from "react-router-dom";

export const launchError = (error) => {
    if (error?.response?.data?.message) {
        toast.error(error.response.data.message);
    }
    if (error?.response?.data) {
        toast.error(error.response.data);
    } else
        toast.error('Unknown Error');
}

export const launchSuccess = (message) => {
    toast.success(message.data);
}

const Layout = () => {
    return (
        <>
            <ToastContainer/>
            <header>
                <nav className="navbar navbar-expand-md navbar-dark bg-dark">
                    <div className="container-fluid">
                        <a className="navbar-brand" href="#">Театр</a>
                        <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav"
                                aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                            <span className="navbar-toggler-icon"></span>
                        </button>
                        <div className="collapse navbar-collapse" id="navbarNav">
                            <ul className="navbar-nav ml-auto">
                                <li className="nav-item">
                                    <a className="nav-link" href={'/'}>Вистави</a>
                                </li>
                                <li className="nav-item">
                                    <a className="nav-link" href={'/orders'}>Замовлення</a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </nav>
            </header>

            <div className="container-fluid">
                <main role="main" className="flex-shrink-0">
                    <div className="container">
                        <Outlet/>
                    </div>
                </main>
            </div>

            <footer className="footer mt-auto py-3 bg-light">
                <div className="container">
                    <span className="text-muted">2023 Лаба 6. Варіант 3.</span>
                </div>
            </footer>
        </>
    )
}

export default Layout;