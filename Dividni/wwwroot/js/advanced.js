let editor;

$(document).ready(() => {
  //For certain pages
  let location = window.location.href;
  if (location.includes('/Advanced/Create')) {
    displayEditorEnabled(true);
  } else if (location.includes('/Advanced/Details') || location.includes('/Advanced/Delete') || location.includes('/Advanced/Share')) {
    displayEditorDisabled();
  } else if (location.includes('/Advanced/Edit') || location.includes('/Advanced/Template')) {
    displayEditorEnabled(false);
  } else if (location.includes('/Advanced/Index') || location.includes('/Advanced?') || location.includes('/Advanced#') || (location.slice(location.length - 8) === 'Advanced')) {
    populateQuestionBankDropdown(JSON.parse(document.getElementById('questionBanks').getAttribute('value')));
  }
  if (location.includes('/Advanced/Share')) {
    M.toast({ html: document.getElementById("message").getAttribute("Value") });
  }
});

displayEditorEnabled = (displaySkeleton) => {
  editor = ace.edit("editor");
  editor.setTheme("ace/theme/monokai");
  editor.session.setMode("ace/mode/csharp");
  if (displaySkeleton === true) {

  } else {

  }
}

displayEditorDisabled = () => {
  editor = ace.edit("editor");
  editor.setTheme("ace/theme/monokai");
  editor.session.setMode("ace/mode/csharp");
}



testAnswer = () => {
  $('#statusModal').modal('open');
  let content = 'Testing answer...';
  document.getElementById('statusModalContent').innerHTML = content;
  document.getElementById('generate').classList.remove("disabled");
}

//When form is submitted, check for required fields and generate the status modal
$("#advancedQuestionForm").submit((event) => {
  event.preventDefault();
  //Display status modal
  $('#statusModal').modal('open');
  let content = '';
  let generate = false;
  //Check that question compiles without errors
  if (generate == true) {
    content ='<p>Display errors.</p>';
  } else {
    content = '<p>Saving question...</p>';
    generate = true;
  }
  document.getElementById('statusModalContent').innerHTML = content;
  if (generate === true) {
    document.getElementById('statusModalClose').classList.add("disabled");
    generateAdvancedQuestion();
  }
});

generateAdvancedQuestion = () => {
  document.getElementById('aspName').value = document.getElementById('name').value;
  document.getElementById('aspModifiedDate').value = new Date().toISOString().slice(0, 10);
  document.getElementById('aspQuestion').value = editor.getValue();
  //Submit the hidden form
  document.getElementById("aspQuestionForm").submit();
}

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


