/// <reference path="knockout-3.5.1.debug.js" />

function AppViewModel() {
    this.math = ko.observableArray(["+", "-", "*", "/"]);
    this.firstValue = ko.observable(0);
    this.secondValue = ko.observable(0);
    this.symbol = ko.observable();

    this.result = ko.computed(function () {
        let firstNumber = parseFloat(this.firstValue());
        let secondNumber = parseFloat(this.secondValue());

        switch (this.symbol()) {
            case "+": return firstNumber + secondNumber;
                break;
            case "-": return firstNumber - secondNumber;
                break;
            case "*": return firstNumber * secondNumber;
                break;
            case "/": return firstNumber / secondNumber;
                break;
            default:
                return "Somhing went wrong :c";
        }
    }, this);
}


ko.applyBindings(new AppViewModel());