/// <reference path="knockout-3.5.1.js" />


function AppViewModel() {

    let self = this;

    ko.validation.init({ insertMessages: false }); //To ensure that only custom error messages are displayed

    //Adding the math operation options to the dropdown menu
    self.math = ko.observableArray(["+", "-", "*", "/"]);

    //Adding observables and validation to the input values
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

    self.resultValue = ko.observable();
    self.dummyValue = ko.observable();

    //when a value changes from the dropdown menu or text box, this function wil fire
    ko.computed(function () {
        console.log(Date.now().toString() + " running ajax");

        //clear existing
        self.resultValue("");

        //It calls the Calculate method from the calculator controller and sends the two values and math symbol
        //to perform the required calculation. If successful, the result will be displayed in the result text box
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
                //$("#result").val(data);
                self.resultValue(data);         //set result value
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