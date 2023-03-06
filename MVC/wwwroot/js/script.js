async function getCars() {
    const preloader = document.getElementById('preloader');
    preloader.style.display = 'block';
    const response = await fetch("http://www.alevelwebsite.com:5001/api/v1/Selection/GetSelection");
    const cars = await response.json();
    const carContainer = document.getElementById("car-container");
    var totalPrice = 0;
    const totalBlock = document.createElement("div");
    totalBlock.innerHTML = "<span>Total sum:</span>";
    totalBlock.classList.add("totalBlock");
    totalBlock.classList.add("car-name");
    carContainer.appendChild(totalBlock);

    const confirmButton = document.createElement("div");
    confirmButton.innerText = "Create order";
    confirmButton.classList.add("confirm-button");
    confirmButton.classList.add("car-name");
    carContainer.appendChild(confirmButton);

    // create the popup element
    const popup = document.createElement("div");
    popup.classList.add("popup");
    popup.innerHTML = `
  <div class="popup-content">
    <p>Do you really want to create an order?</p>
    <div class="popup-buttons">
      <button class="popup-button-yes">Yes</button>
      <button class="popup-button-no">No</button>
    </div>
  </div>
`;

    // add event listener to show the popup when the confirm button is clicked
    confirmButton.addEventListener("click", () => {
        // add the popup to the body
        document.body.appendChild(popup);

        // add a class to the body to disable scrolling while the popup is open
        document.body.classList.add("no-scroll");

        // add event listeners to the buttons to handle closing the popup
        const yesButton = popup.querySelector(".popup-button-yes");
        const noButton = popup.querySelector(".popup-button-no");
        yesButton.addEventListener("click", () => {
            window.location.href = "http://www.alevelwebsite.com:5001/Order.html";
        });
        noButton.addEventListener("click", () => {
            document.body.removeChild(popup);
            document.body.classList.remove("no-scroll");
        });
    });





    cars.forEach((car) => {

        const id = document.createElement("div");
        id.innerText = car.carId;

        const carBlock = document.createElement("div");
        carBlock.classList.add("car-block");

        const carImage = document.createElement("img");
        carImage.src = car.imageFileName + ".jpg";
        carImage.alt = car.carName;
        carImage.classList.add("car-image");

        const carDetails = document.createElement("div");
        carDetails.classList.add("car-details");

        const carName = document.createElement("div");
        carName.innerText = car.carName;
        carName.classList.add("car-name");

        const carPrice = document.createElement("div");
        carPrice.innerText = `Price: $${car.price}`;
        carPrice.classList.add("car-price");

        totalPrice += car.price

        const carType = document.createElement("div");
        carType.innerText = `Type: ${car.carType.typeName}`;
        carType.classList.add("car-type");
        carType.addEventListener('mouseenter', function () {
            carType.innerText = `Type: ${car.carType.typeName} (${car.carType.typeDescription})`;
        });
        carType.addEventListener('mouseleave', function () {
            carType.innerText = `Type: ${car.carType.typeName}`;
        });

        const carManufacturer = document.createElement("div");
        carManufacturer.innerText = `Manufacturer: ${car.manufacturer.manufacturerName} (${car.manufacturer.manufacturerCountry})`;
        carManufacturer.classList.add("car-manufacturer");

        const carAvailability = document.createElement("div");
        carAvailability.innerText = `Availability: ${car.availability}`;
        carAvailability.classList.add("car-availability");

        const carPromo = document.createElement("div");
        carPromo.innerText = `Promo: ${car.carPromo}`;
        carPromo.classList.add("car-promo");

        const closeLink = document.createElement("a");
        closeLink.href = "#";
        closeLink.classList.add("close");
        closeLink.addEventListener('click', function () {
            const id = car.carId;
            const apiUrl = `http://www.alevelwebsite.com:5001/api/v1/Selection/AddItemToSelection/?id=${id}&mode=false`;
            fetch(apiUrl, { method: 'POST' })
                .then(response => {
                    if (response.ok) {
                        carBlock.remove();
                        totalPrice -= car.price;
                        totalBlock.innerText = "Total sum: $" + totalPrice;
                        if (carContainer.childElementCount === 2) {
                            window.location.href = "http://www.alevelwebsite.com:5001";
                        }
                    } else {
                        throw new Error(`Failed to remove car with ID ${id} from selection`);
                    }
                })
                .catch(error => {
                    console.error(error);
                    alert(`Failed to remove car with ID ${id} from selection`);
                });
        });

        carBlock.appendChild(carImage);
        carDetails.appendChild(carName);
        carDetails.appendChild(carPrice);
        carDetails.appendChild(carType);
        carDetails.appendChild(carManufacturer);
        carDetails.appendChild(carAvailability);
        carBlock.appendChild(carDetails);
        carBlock.appendChild(carPromo);
        carBlock.appendChild(closeLink);
        carContainer.appendChild(carBlock);

        // carContainer.appendChild(id);
        preloader.style.display = 'none';
    });

    if (totalPrice != 0) totalBlock.innerHTML = "<span>Total sum: $" + totalPrice + "</span>";
    else totalBlock.style.display = "none";
}
   
getCars();