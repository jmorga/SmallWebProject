﻿/// <reference path="knockout-3.5.1.js" />

//Person class to store the id, first name and last name of a person.
function person(completeName) {
    let self = this;

    self.id = ko.observable(completeName.id);
    self.firstName = ko.observable(completeName.firstName);
    self.lastName = ko.observable(completeName.lastName);

    //Calling controller to update the table in the dabase
    self.updateTable = function () {

        $.ajax({
            type: "POST",
            url: "/Database/ChangePeople",
            timeout: 6000,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                'id': self.id(),
                'firstName': self.firstName(),
                'lastName': self.lastName()
            }),
            dataType: "json",
            success: function (data) {
                if (data.result) {
                    console.log(data.message);
                }
                else {
                    alert(data.message);
                }
            },
            error: function () {
                alert("Unable to save Data");
            }
        });
    }

    //Subscribing the observables updateTable function
    self.firstName.subscribe(self.updateTable);
    self.lastName.subscribe(self.updateTable);
}

function databaseViewModel() {
    let self = this;

    self.personList = ko.observableArray();

    //document.addEventListener("click", function () {
    //    document.getElementById("confirmationMsg").innerHTML = "";
    //});

    //Stablish a connection with TableHub to know when the Person table changed.
    $(function () {
        let updates = $.connection.tableHub;

        //If the values of the table changes, reload the data
        updates.client.newUpdate = function () {
            console.log("The database has changed");
            checkUpdate();
        }

        $.connection.hub.start()
            .done(function () { console.log("Connection stablished") })
            .fail(function () { alert("connection failed at Hub") });
    });

    //It calls the GetPeople method from the database controller to get an array with the data from
    //the Person table in the People database
    $.ajax({
        type: "POST",
        url: "/Database/GetPeople",
        timeout: 6000,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.result) {
                setTable(data.jsonStr);
            }
            else {
                alert(data.jsonStr);
            }
        },
        error: function () {
            alert("Could not stablish a connection with the database");
        }
    });

    //When the ajax call gets the data, it calls this function to add the data into the list to be displayed in the web page
    function setTable(data) {

        data = JSON.parse(data);

        data.forEach(function (entry) {
            self.personList.push(new person(entry));
        });
    };

    function checkUpdate() {

        $.ajax({
            type: "POST",
            url: "/Database/GetUpdate",
            //timeout: 6000,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ list: ko.toJSON(self.personList) }),
            dataType: "json",
            success: function (newData) {
                updateTable(newData);
            },
            error: function () {
                alert("Could not check update");
            }
        });
    };

    function updateTable(data) {
        data = JSON.parse(data);

        data.forEach(function (entry) {
            self.personList.remove(function (item) { return item.id() == entry.id });
            self.personList.push(new person(entry));
        });
    };
}

ko.applyBindings(new databaseViewModel());