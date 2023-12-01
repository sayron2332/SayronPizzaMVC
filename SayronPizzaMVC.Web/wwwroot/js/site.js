let button = document.querySelector(".card__button button");

let row = document.querySelector(".main__content");
let basket = document.querySelector(".basket__order");

console.log(row);

let id = 0;

row.addEventListener("click", function (event) {
    if (event.target.closest('.card__button')) {
        const product = event.target.closest('.card');

        let productInfo = {
            img: product.querySelector('.card__photo img').getAttribute('src'),
            name: product.querySelector('.card__title').innerText,
            product: product.querySelector('.card__about').innerText,
            size: product.querySelector('.card__size').innerText,
            price: product.querySelector('.card__priceContent').innerText,
        }

        let productHtml = `<div class="basket__orderItem">
        <div class="basket__orderImg">
            <img src=" ${productInfo.img}" alt="">
        </div>

        <div class="basket__orderName">
        ${productInfo.name}
        </div>

        <div class="basket__orderPrice">
        ${productInfo.price}
        </div>

        <div class="basket__size">
        ${productInfo.size}
        </div>

        <div class="basket__Subtotal">
        ${productInfo.price}
        </div>
    </div>`;

        console.log(productInfo);

        basket.insertAdjacentHTML('beforeend', productHtml);

       
    }

});

