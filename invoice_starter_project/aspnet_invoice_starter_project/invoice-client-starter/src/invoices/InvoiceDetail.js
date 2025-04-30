

import React, {useEffect, useState} from "react";
import {useParams} from "react-router-dom";

import {apiGet} from "../utils/api";


const InvoiceDetail = () => {
    const {id} = useParams();
    const [invoice, setInvoice] = useState({});
 
    useEffect(() => {
        apiGet("/api/Invoices/"+id)
        .then((data)=>{
            setInvoice(data)
        })
        .catch ((error)=> {
            console.error(error);
        });
    }, [id]);


    return (
        <>
            <div>
                <h1>Detail faktury</h1>
                <hr/>
                <h3>{invoice.product} - {invoice.invoiceNumber}</h3>
                <p>
                    <strong>Vytvořena:</strong>
                    <br/>
                    {invoice.issued}
                </p>
                <p>
                    <strong>Splatnost:</strong>
                    <br/>
                    {invoice.dueTime}
                </p>
                <p>
                    <strong>Cena:</strong>
                    <br/>
                    {invoice.price}
                </p>
                <p>
                    <strong>DPH:</strong>
                    <br/>
                    {invoice.vat}
                </p>
                <p>
                    <strong>Prodávající:</strong>
                    <br/>
                    {/* <pre>{JSON.stringify(invoice.seller, null, 2)}</pre> */}
                    {invoice.seller?.name}
                    {/* , 
                    {person.city},
                    {person.zip}, {country} */}
                </p>
                <p>
                    <strong>Kupující:</strong>
                    <br/>
                    {invoice.buyer?.name}
                </p>
                <p>
                    <strong>Poznámka:</strong>
                    <br/>
                    {invoice.note}
                </p>
            </div>
        </>
    );
};

export default InvoiceDetail;
