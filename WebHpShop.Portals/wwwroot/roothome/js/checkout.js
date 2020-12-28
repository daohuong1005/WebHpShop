$(document).ready(function () {

    const numberFormat = new Intl.NumberFormat('vi-VN', {
        style: 'currency',
        currency: 'VND',
    });

    $('#Select-option-address').change(function() {
        if ($("#Select-option-address option:selected").val() == 'address-default-option') {
            $('.address-new-order').slideUp();
        } else if ($("#Select-option-address option:selected").val() == 'address-new-option') {
            $('.address-new-order').slideDown();
        }
    });
    

    function CheckBlur(value, Id, tag) {
        Id = "#" + Id;
        if (value.length == 0) {
            $(Id).html(tag + " không được trống");
        } else {
            $(Id).html("");
        }
    }

    $("#txtLastName").blur(function () {
        var LastName = $("#txtLastName").val();
        CheckBlur(LastName, "error-LastName", "Họ đệm");
    });

    $("#txtFirstName").blur(function () {
        var FName = $("#txtFirstName").val();
        CheckBlur(FName, "error-FirstName", "Tên");
    });

    $("#txtPhone").blur(function () {
        var Phone = $("#txtPhone").val();
        CheckBlur(Phone, "error-Phone", "Số Điện Thoại");
    });

    $("#txtEmail").blur(function () {
        var EMail = $("#txtEmail").val();
        CheckBlur(EMail, "error-Email", "Email");
    });

    $("#txtAddress").blur(function () {
        var Address = $("#txtAddress").val();
        CheckBlur(Address, "error-Address", "Địa Chỉ");
    });

    $("#txtCity").blur(function () {
        var City = $("#txtCity").val();
        CheckBlur(City, "error-City", "Thành phố");
    });

    $('.btnBuyProductinCheckOut').click(function(){
        if($( "#Select-option-address option:selected" ).val()=='address-new-option'){  
            var LastName = $("#txtLastName").val();
            var FName = $("#txtFirstName").val();
            var Phone = $("#txtPhone").val();
            var EMail = $("#txtEmail").val();
            var Address = $("#txtAddress").val();
            var City = $("#txtCity").val();

            if(LastName.length == 0){
                $('#error-LastName').html("Họ đệm không được trống");
                alert("Họ đệm không được trống");
                return false;
            } else if(FName.length == 0){
                $('#error-FirstName').html("Tên không được trống");
                alert("Tên không được trống");
                return false;
            } else if(Phone.length == 0){
                $('#error-Phone').html("Số điện thoại không được trống");
                alert("Số điện thoại không được trống");
                return false;
            } else if(EMail.length == 0){
                $('#error-Email').html("Email không được trống");
                alert("Email không được trống");
                return false;
            } else if(Address.length == 0){
                $('#error-Address').html("Địa chỉ không được trống");
                alert("Địa chỉ không được trống");
                return false;
            } else if(City.length == 0){
                $('#error-City').html("Thành phố không được trống");
                alert("Thành phố không được trống");
                return false;
            }
        }
    });

    var Quantity = document.getElementsByClassName('quantity_a_product');
    var SumQuantityOrder = 0;
    for(var i = 0; i < Quantity.length; i++)
    {
        SumQuantityOrder += parseInt(Quantity[i].innerText);
    }
    document.getElementById('Sum_Quantity_Order').textContent = SumQuantityOrder;


    var GetValueMoney = document.getElementsByClassName("cart_total_price");
    var SumMoney = 0;
    for(var i = 0; i < GetValueMoney.length; i++)
    {
        SumMoney += parseInt(GetValueMoney[i].innerText.replace('.','').substring(0,GetValueMoney[i].textContent.length-2));
    }
        document.getElementById('Sum_Money_Of_Cart').textContent = numberFormat.format(SumMoney);
});
