$(document).ready(() => {
    //For certain pages
    let location = window.location.href;
    if (location.includes('/Advanced/Index') || location.includes('/Advanced?') || location.includes('/Advanced#') || (location.slice(location.length - 8) === 'Advanced')) {
        populateQuestionBankDropdown(JSON.parse(document.getElementById('questionBanks').getAttribute('value')));
    }
});


