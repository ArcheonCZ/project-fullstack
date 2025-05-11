

export const dateStringFormatter = (str, locale = false) => {
    const d = new Date(str);
//console.log("dateStringFormatter, str: "+str)
//console.log("dateStringFormatter, d: "+d)
    if (locale) {
        return d.toLocaleDateString("cs-CZ", {
            year: "numeric",
            month: "long",
            day: "numeric",
        });
    }

    const year = d.getFullYear();
    const month = "" + (d.getMonth() + 1);
    const day = "" + d.getDate();

    return [
        year,
        month.length < 2 ? "0" + month : month,
        day.length < 2 ? "0" + day : day,
    ].join("-");
};

export default dateStringFormatter;