
$(document).ready(function () {


    //Javascript

    //product details

        $(".minus").click(function () {
            var value = document.getElementById("input-qty").value;
            if (value <= 1) {
                document.getElementById("input-qty").value = 1;
            }
            else {
                value -= 1;
                document.getElementById("input-qty").value = value;
            }
        })

        $(".plus").click(function () {
            var value = document.getElementById("input-qty").value;
            if (value >= 999) {
                document.getElementById("input-qty").value = 999;
            }
            else {
                value = parseInt(value) + 1;
                document.getElementById("input-qty").value = value;
            }
        })

        $("#input-qty").blur(function () {
            var value = document.getElementById("input-qty").value;
            if (value >= 999) {
                document.getElementById("input-qty").value = 999;
            }
            if (value <= 0) {
                document.getElementById("input-qty").value = 1;
            }
        })




    //cart
    const numberFormat = new Intl.NumberFormat('vi-VN', {
        style: 'currency',
        currency: 'VND',
    });

    $(".cart_quantity_down").click(function () {
        var quantity = document.getElementById('cart_quantity_input').value;
        if (quantity <= 1) {
            document.getElementById("cart_quantity_input").value = 1;
        }
        else {
            quantity -= 1;
            document.getElementById("cart_quantity_input").value = quantity;
        }

        quantity = document.getElementById('cart_quantity_input').value;
        var price = document.getElementById('price-a-product').innerText.replace('.', '');
        document.getElementById('total_price_a_products').innerText = numberFormat.format(quantity * price.substring(0, price.length - 2));
        SumMoneyOfCart();
    })

    $(".cart_quantity_up").click(function () {
        var quantity = document.getElementById('cart_quantity_input').value;
        if (quantity >= 999) {
            document.getElementById("cart_quantity_input").value = 999;
        }
        else {
            quantity = parseInt(quantity) + 1;
            document.getElementById("cart_quantity_input").value = quantity;
        }

        quantity = document.getElementById('cart_quantity_input').value;
        var price = document.getElementById('price-a-product').innerText.replace('.', '');
        document.getElementById('total_price_a_products').innerText = numberFormat.format(quantity * price.substring(0, price.length - 2));
        SumMoneyOfCart();
    })

    $("#cart_quantity_input").blur(function () {
        var quantity = document.getElementById('cart_quantity_input').value;
        if (quantity >= 999) {
            document.getElementById("cart_quantity_input").value = 999;
        }
        if (quantity <= 1) {
            document.getElementById("cart_quantity_input").value = 1;
        }
        quantity = document.getElementById('cart_quantity_input').value;
        var price = document.getElementById('price-a-product').innerText.replace('.', '');
        document.getElementById('total_price_a_products').innerText = numberFormat.format(quantity * price.substring(0, price.length - 2));
        SumMoneyOfCart();
    })

    SumMoneyOfCart();
    function SumMoneyOfCart() {
        var GetValue = document.getElementsByClassName("cart_total_price");
        var SumMoney = 0;
        for (var i = 0; i < GetValue.length; i++) {
            SumMoney += parseInt(GetValue[i].innerText.replace('.', ''));
        }
        document.getElementById('Total_Money_Of_Cart').innerText = numberFormat.format(SumMoney);
    }
})