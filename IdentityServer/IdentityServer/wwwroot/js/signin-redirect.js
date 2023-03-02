console.log("TEst");
console.log(document.querySelector("meta[http-equiv=refresh]").getAttribute("data-url"));
window.location.href = document.querySelector("meta[http-equiv=refresh]").getAttribute("data-url");