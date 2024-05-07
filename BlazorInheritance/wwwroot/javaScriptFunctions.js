async function GetElementHtmlText(elementID) {
    await setTimeout(() => { }, 1);
    console.log(elementID);
    var element = document.getElementById(elementID);
    console.log(element);
    var result = element.innerHTML;
    console.log(result);
    return result;
}
