import React, {useEffect, useState} from "react";
import {apiDelete, apiGet} from "../utils/api";
import InvoiceTable from "./InvoiceTable";
import Statistics from "../statistics/StatisticsOverAll";

const InvoiceIndex = () => {
    const [invoices, setInvoices] = useState([]);

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
    }, []);

    return (
        <div>
            <Statistics/>
            <h1>Seznam faktur</h1>
            <InvoiceTable
                deleteInvoice={deleteInvoice}
                items={invoices}
                label="Počet faktur:"
            />
        </div>
    );
};
export default InvoiceIndex;
