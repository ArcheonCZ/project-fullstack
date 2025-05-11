

import React, {useEffect, useState} from "react";
import {useNavigate, useParams} from "react-router-dom";

import {apiGet, apiPost, apiPut} from "../utils/api";

import InputField from "../components/InputField";
import FlashMessage from "../components/FlashMessage";
import InputSelect from "../components/InputSelect";
import dateStringFormatter from "../utils/dateStringFormatter";

const InvoiceForm = () => {
    const navigate = useNavigate();
    const {id} = useParams();
     const [invoice, setInvoice] = useState({
        invoiceNumber: "",
        seller: "",
        buyer: "",
        issued: "",
        dueTime: "",
        product: "",
        price: "",
        vat: "",
        note: ""
    });
    const [sentState, setSent] = useState(false);
    const [successState, setSuccess] = useState(false);
    const [errorState, setError] = useState(null);
    const [persons,setPersons] = useState([]);

    useEffect(() => {
        if (id) {
            apiGet("/api/invoices/" + id).then((data) => setInvoice(data));
            
        }
    }, [id]);
    useEffect(() => {
            apiGet("/api/persons").then((data) => setPersons(data));
            console.log(invoice.dueTime)
            console.log(invoice.name)
    }, []);

    ///nastavení prvního záznamu jako defaultní

    // useEffect(() => {
    //     if (persons && persons.length > 0) {
    //         setInvoice((prev) => ({
    //             ...prev,
    //             seller: prev.seller ? { _id: prev.seller._id } : { _id: persons[0]._id },
    //             buyer: prev.buyer ? { _id: prev.buyer._id } : { _id: persons[0]._id },
    //          }));
            
    //     }
    //     else {
    //         console.log("persons je nula...(nenastavil jsem první záznam jako defaultní");

    //     }
    // }, [persons]);

    const handleSubmit = (e) => {
        e.preventDefault();

        (id ? apiPut("/api/invoices/" + id, invoice) : apiPost("/api/invoices", invoice))
            .then((data) => {
                setSent(true);
                setSuccess(true);
                navigate("/invoices");
                console.log("odeslaná faktura: ", invoice);
            })
            .catch((error) => {
                console.log(error.message);
                setError(error.message);
                setSent(true);
                setSuccess(false);
            });
    };

    const sent = sentState;
    const success = successState;

    return (
        <div>
            <h1>{id ? "Upravit" : "Vytvořit"} fakturu</h1>
            <hr/>
            {errorState ? (
                <div className="alert alert-danger">{errorState}</div>
            ) : null}
            {sent && (
                <FlashMessage
                    theme={success ? "success" : ""}
                    text={success ? "Uložení faktury proběhlo úspěšně." : ""}
                />
            )}
            <form onSubmit={handleSubmit}>
                <InputField
                    required={true}
                    type="text"
                    name="invoiceNumber"
                    min="3"
                    label="Číslo faktury"
                    prompt="Zadejte číslo faktury"
                    value={invoice.invoiceNumber}
                    handleChange={(e) => {
                        setInvoice({...invoice, invoiceNumber: e.target.value});
                    }}
                />

                <InputField
                    required={true}
                    type="date"
                    name="issued"
                    min="3"
                    label="Datum vystavení"
                    prompt="Zadejte datum vystavení faktury"
                    value={dateStringFormatter(invoice.issued)}
                    handleChange={(e) => {
                        setInvoice({...invoice, issued: e.target.value});
                    }}
                />

                <InputField
                    required={true}
                    type="date"
                    name="dueTime"
                    min="3"
                    label="Datum splatnosti"
                    prompt="Zadejte datum splatnosti faktury"
                    value={dateStringFormatter(invoice.dueTime)}
                    handleChange={(e) => {
                        setInvoice({...invoice, dueTime: e.target.value});
                    }}
                />

                <InputField
                    required={true}
                    type="number"
                    name="price"
                    label="Cena"
                    prompt="Zadejte cenu"
                    value={invoice.price}
                    handleChange={(e) => {
                        setInvoice({...invoice, price: e.target.value});
                    }}
                />

                <InputField
                    required={true}
                    type="number"
                    name="vat"
                    label="DPH"
                    prompt="Zadejte výši DPH"
                    value={invoice.vat}
                    handleChange={(e) => {
                        setInvoice({...invoice, vat: e.target.value});
                    }}
                />
                  <InputField
                    required={true}
                    type="text"
                    name="note"
                    label="Název"
                    prompt="Zadejte název služby"
                    value={invoice.product}
                    handleChange={(e) => {
                        setInvoice({...invoice, product: e.target.value});
                    }}
                />
                <InputField
                    required={true}
                    type="text"
                    name="note"
                    label="Poznámka"
                    prompt="Zadejte poznámku"
                    value={invoice.note}
                    handleChange={(e) => {
                        setInvoice({...invoice, note: e.target.value});
                    }}
                />

                <InputSelect
                    name="buyer"
                    label="Odběratel"
                    items={persons}
                    prompt="Vyberte odběratele"
                    value={invoice.buyer?._id ?? ""}
                    required={false}
                    handleChange={(e) => {
                        console.log(e.target.value);
                        // const selectedBuyer = persons.find(p => p._id === Number(e.target.value));
                        // if (!selectedBuyer) {
                        //     console.warn("Nebyl nalezen žádný buyer s ID", e.target.value);
                        // }
                        // console.log("Vybraný buyer:", selectedBuyer);
                        // setInvoice({ ...invoice, buyer: selectedBuyer || null });
                        setInvoice({...invoice,buyer: { _id: Number(e.target.value) }});
                    }}
                    />  
                <InputSelect
                    name="seller"
                    label="Dodavatel"
                    items={persons}
                    prompt="Vyberte dodavatele"
                    value={invoice.seller?._id ?? ""}
                    required={false}
                    handleChange={(e) => {
                    console.log(e.target.value);
                    setInvoice({...invoice,seller: { _id: Number(e.target.value) }});
                    }}
                /> 
               
               
                <input type="submit" className="btn btn-primary my-2" value="Uložit"/>
            </form>
        </div>
    );
};

export default InvoiceForm;
