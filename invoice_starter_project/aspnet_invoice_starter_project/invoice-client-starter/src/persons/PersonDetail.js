

import React, {useEffect, useState} from "react";
import {useParams} from "react-router-dom";

import {apiGet} from "../utils/api";
import Country from "./Country";
import PersonStatistics from "./PersonStatistics";

const PersonDetail = () => {
    const {id} = useParams();
    const [person, setPerson] = useState({});

    useEffect(() => {
        apiGet("/api/persons/"+id)
        .then((data)=>{
            setPerson(data)
        })
        .catch ((error)=> {
            console.error(error);
        });
    }, [id]);
    //console.log("person detail - identificationNumber: "+person.identificationNumber)
    const country = Country.CZECHIA === person.country ? "Česká republika" : "Slovensko";

    return (
        <>
            {/* <div className="row"> */}
                <div >
                <h1>Detail osoby</h1>
                <hr/>
                <h3>{person.name} ({person.identificationNumber})</h3>
                <p>
                    <strong>DIČ:</strong>
                    <br/>
                    {person.taxNumber}
                </p>
                <p>
                    <strong>Bankovní účet:</strong>
                    <br/>
                    {person.accountNumber}/{person.bankCode} ({person.iban})
                </p>
                <p>
                    <strong>Tel.:</strong>
                    <br/>
                    {person.telephone}
                </p>
                <p>
                    <strong>Mail:</strong>
                    <br/>
                    {person.mail}
                </p>
                <p>
                    <strong>Sídlo:</strong>
                    <br/>
                    {person.street}, {person.city},
                    {person.zip}, {country}
                </p>
                <p>
                    <strong>Poznámka:</strong>
                    <br/>
                    {person.note}
                </p>
                </div>
            <div>
                <PersonStatistics identificationNumber={person.identificationNumber} type="purchases"/>
            </div>
            <div>
                <PersonStatistics identificationNumber={person.identificationNumber} type="sales"/>
            </div>
             {/* </div> */}
        </>
    );
};

export default PersonDetail;
