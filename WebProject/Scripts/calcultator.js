/// <reference path="knockout-3.5.1.js" />


function AppViewModel() {

    let self = this;

    ko.validation.init({ insertMessages: false });

    self.math = ko.observableArray(["+", "-", "*", "/"]);
   // this.firstValue = ko.observable(0);
   // self.secondValue = ko.observable(0);

    self.firstValue = ko.observable("0").extend({
        required: true,
        validation: {
            message: "Please enter a valid number",
            validator: function (value) {
                return !isNaN(value);
            }
        }
    });

    self.secondValue = ko.observable("0").extend({
        required: true,
        validation: {
            message: "Please enter a valid number",
            validator: function (value) {
                return !isNaN(value);
            }
        }
    });

    self.symbol = ko.observable();

    ko.computed(function () {


        $.ajax({
            type: "POST",
            url: "/Calculator/Calculate",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                'firstNumber': this.firstValue(),
                'secondNumber': this.secondValue(),
                'mathOperation': this.symbol()
            }),
            dataType: "json",
            success: function (data) {
                $("#result").val(data);
            },
            error: function () {
                alert("Failed at math operaton");
            }
        });

        //let firstNumber = parseFloat(this.firstValue());
        //let secondNumber = parseFloat(this.secondValue());

        //switch (this.symbol()) {
        //    case "+": return firstNumber + secondNumber;
        //        break;
        //    case "-": return firstNumber - secondNumber;
        //        break;
        //    case "*": return firstNumber * secondNumber;
        //        break;
        //    case "/": return firstNumber / secondNumber;
        //        break;
        //    default:
        //        return "Somhing went wrong :c";
        //}
    }, this);
}


ko.applyBindings(new AppViewModel());