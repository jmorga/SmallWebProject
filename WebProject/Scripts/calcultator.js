/// <reference path="knockout-3.5.1.debug.js" />

function AppViewModel() {
    this.math = ko.observableArray(["+", "-", "*", "/"]);
    this.firstValue = ko.observable(0);
    this.secondValue = ko.observable(0);
    this.symbol = ko.observable();

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
                return "fail";
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