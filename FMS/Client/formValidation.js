import $ from 'jquery';
import 'jquery-validation';
import 'jquery-validation-unobtrusive';

// Change validation classes to work with Bootstrap
const settings = {
    validClass: "is-valid",
    errorClass: "is-invalid"
};
$.validator.setDefaults(settings);
$.validator.unobtrusive.options = settings;
