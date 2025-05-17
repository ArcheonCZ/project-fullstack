import React, {useEffect, useState} from "react";
import {apiDelete, apiGet} from "../utils/api";
import InvoiceTable from "./InvoiceTable";
import Statistics from "../statistics/StatisticsOverAll";
import InvoiceFilter from "./InvoiceFilter";

const InvoiceIndex = () => {
    const [invoices, setInvoices] = useState([]);
    // const [sellerListState, setSellerList] = usestate([]);
    // const [buyerListState, setBuyerList] = usestate([]);
    const [personsListState, setPersonsList] = useState([]);
    const [filterState, setFilter]= useState({
        sellerId:undefined,
        buyerId:undefined,
        minPrice:undefined,
        product:undefined,
        limit: undefined,
    });

    const deleteInvoice = async (id) => {
        try {
            await apiDelete("/api/Invoices/" + id);
        } catch (error) {
            console.log(error.message);
            alert(error.message)
        }
        setInvoices(invoices.filter((item) => item._id !== id));
         alert("Smazána faktura s id: "+id);
    };

    useEffect(() => {
        apiGet("/api/Invoices").then((data) => setInvoices(data));
        apiGet("/api/Persons").then((data) => setPersonsList(data));
    }, []);

    ///snaha o automatické načítání hodnot z filteru - vyzkoušet
    // useEffect(() => {
    //       apiGet("/api/invoices", filterState); 
    // },[filterState]);

    const handleChange = (e) => {
    // pokud vybereme prázdnou hodnotu (máme definováno jako true/false/'' v komponentách), nastavíme na undefined
        if (e.target.value === "false" || e.target.value === "true" || e.target.value === '') {
            setFilter(prevState => {
                return {...prevState, [e.target.name]: undefined}
            });
        } else {
            setFilter(prevState => {
                return { ...prevState, [e.target.name]: e.target.value}
            });
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const params = filterState;
        const data = await apiGet("/api/invoices", params); 
        setInvoices(data);
    };

        return (
            <div>
                <Statistics/>
                <hr/>
                <div className="row">
                    <div className="col-2 my-5">
                        <div className="my-4">
                        <InvoiceFilter
                            handleChange={handleChange}
                            handleSubmit={handleSubmit}
                            personsList={personsListState}
                            filter={filterState}
                            confirm="Filtrovat filmy"
                        />
                        </div>
                    </div>
                    <div className="col-10">
                        
                        <h1>Seznam faktur</h1>
                        <InvoiceTable
                            deleteInvoice={deleteInvoice}
                            items={invoices}
                            label="Počet faktur:"
                        />
                    </div>
                </div>
            </div>
        );
};
export default InvoiceIndex;
