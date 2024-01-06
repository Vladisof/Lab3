import './App.css';
import {BrowserRouter, Routes, Route} from "react-router-dom";
import Layout from "./components/layout/Layout";
import Orders from "./pages/Orders";
import Spectacles from "./pages/Spectacles";

function App() {
    return (
        <>
            <BrowserRouter>
                <Routes>
                    <Route path="/" element={<Layout/>}>
                        <Route path={"/"} element={<Spectacles/>}/>
                        <Route path={"/orders"} element={<Orders/>}/>
                    </Route>
                </Routes>
            </BrowserRouter>
        </>
    );
}

export default App;
