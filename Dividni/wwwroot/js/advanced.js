$(document).ready(() => {
    //For certain pages
    let location = window.location.href;
    if (location.includes('/Advanced/Details') || location.includes('/Advanced/Delete') || location.includes('/Advanced/Share')) {
        //displayQuestionHTML();
    } else if (location.includes('/Advanced/Index') || location.includes('/Advanced?') || location.includes('/Advanced#') || (location.slice(location.length - 8) === 'Advanced')) {
        populateQuestionBankDropdown(JSON.parse(document.getElementById('questionBanks').getAttribute('value')));
    }
    if (location.includes('/Advanced/Share')) {
        M.toast({ html: document.getElementById("message").getAttribute("Value") });
      }
});

//Submit hidden form, but first check the user email
shareAdvancedQuestionForm = (event) => {
    event.preventDefault();
    let email = event.target.elements.email.value;
    let currentUserEmail = document.getElementById("aspUserEmail").value;
    //Prevent user from sharing question with himself/herself
    if (email === currentUserEmail) {
      document.getElementById("email").value = "";
      M.toast({ html: 'You cannot share a question with yourself.' })
    } else {
      //Update the necessary fields and submit the form
      document.getElementById("aspUserEmail").value = email;
      document.getElementById('aspModifiedDate').value = new Date().toISOString().slice(0, 10);
      document.getElementById("aspQuestionForm").submit();
    }
  }


