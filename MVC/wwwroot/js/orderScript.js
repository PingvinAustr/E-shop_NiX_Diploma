
async function getCars() {
    const response = await fetch("http://www.alevelwebsite.com:5001/api/v1/Selection/GetSelection");
    const cars = await response.json();
    console.log(cars);

    const responseOrder = await fetch(`http://www.alevelwebsite.com:5003/api/v1/BasketBff/CreateOrder?plainTextData=` + JSON.stringify(cars));
    const result = await responseOrder.json();
    console.log("result");
    console.log(result);

    const responseAccount = await fetch("http://www.alevelwebsite.com:5001/Account/AccountData");
    const accountData = await responseAccount.json();
    console.log("account");
    console.log(accountData);

    document.getElementById("name").innerHTML = accountData.given_name;
    document.getElementById("surname").innerHTML = accountData.family_name;

    document.getElementById("order-id").innerHTML = result['orderId'];

    const date = new Date(result['dateTime']);
    const options = { year: 'numeric', month: '2-digit', day: '2-digit' };
    const formattedDate = date.toLocaleDateString('en-US', options);
    document.getElementById("order-date").innerHTML = formattedDate;

    document.getElementById("order-count").innerHTML = 'x'+result['orderCount'];
    document.getElementById("order-price").innerHTML = result['totalSum'];
    document.getElementById("preloader-img").src = "http://www.alevelwebsite.com:81/assets/images/checkmark1.png";

    // Populate accordion with cars data
    const accordion = document.getElementById("accordion");
    cars.forEach(car => {
        const carHtml = `
            <button class="accordion">${car.carName}</button>
            <div class="panel">
                <h3><p><img style='width:1000px; height:600px' src='${car.imageFileName + '.jpg'}'/></p></h3>
                <h3><p>Manufacturer: ${car.manufacturer.manufacturerName}</p></h3>
                <h3><p>Type: ${car.carType.typeName}</p></h3>
                <h3><p>Promo: ${car.carPromo}</p></h3>
                <h3><p>Price: $${car.price} </p></h3>
            </div>
        `;
        accordion.innerHTML += carHtml;
    });

    // Add click event listener to accordion buttons
    const accordionButtons = document.querySelectorAll(".accordion");
    accordionButtons.forEach(button => {
        button.addEventListener("click", function () {
            this.classList.toggle("active");
            const panel = this.nextElementSibling;
            if (panel.style.display == "block") {
                panel.style.display = "none";
            } else {
                panel.style.display = "block";
            }
        });
    });
}

getCars();
