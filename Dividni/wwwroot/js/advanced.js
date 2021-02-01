let editor;

$(document).ready(() => {
  //Set-up code editor
  if (document.getElementById('editor') != null) {
    editor = ace.edit("editor");
    editor.setTheme("ace/theme/chrome");
    editor.session.setMode("ace/mode/csharp");
    document.getElementById('editor').style.fontSize = '14px';
  }
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
  if (displaySkeleton === true) {
    let skeletonCode = `using System;\r\nusing System.Text;\r\n\r\nnamespace Utilities.Courses\r\n{\r\n   public partial class QHelper : IQHelper\r\n   {\r\n      public static string /*QuestionId*/(Random random, Action<string, ushort> registerAnswer, bool isProof) \r\n      {\r\n         var q = new /*Truth or Xyz*/Question(random, isProof); \r\n         q.Id = \"\"; //QuestionId\r\n         q.Marks = 1; //QuestionMarks\r\n         q.Stem = \"\"; //QuestionText\r\n         q.AddCorrects(\r\n            \r\n         ); \r\n         q.AddIncorrects(\r\n   \r\n         ); \r\n         string rval = q.GetQuestion(registerAnswer);\r\n         return rval;\r\n      } \r\n   } \r\n}\r\n`;
    editor.setValue(skeletonCode, 1);
  } else { //Display existing question
    editor.setValue(JSON.parse(document.getElementById('editor').getAttribute('value')), 1);
  }
}

displayEditorDisabled = () => {
  editor.setOption("readOnly", true); //Read only
  //Display question in the editor
  editor.setValue(JSON.parse(document.getElementById('editor').getAttribute('value')), 1);
}

testAnswer = () => {
  $('#statusModal').modal('open');
  let content = '';
  //Check name matches regex 
  let name = document.getElementById('name').value;
  let re = new RegExp('^[a-zA-Z][a-zA-Z0-9]*');
  if (!(re.test(name))) {
    content = 'Name not suitable - please check the tutorial.';
  } else if (editor.getValue() == "") { //Check code editor not empty
    content += 'Please add some code to test.';
  } else {
    content = 'Compiling code...';
    document.getElementById('statusModalContent').innerHTML = content;
    //Send the code to the api for testing
    $.ajax({
      url: 'https://localhost:5003//api/Dividni/CompileQuestion',
      method: 'POST',
      data: { 'name': name, 'question': JSON.stringify(editor.getValue()) },
      success: (res) => {
        if (res == 'Success') {
          console.log('Success');
          document.getElementById('generate').classList.remove("disabled");
        } else {
          console.log(res);
        }
      },
      error: (err) => {
        console.log(err);
      }
    });
  }
  document.getElementById('statusModalContent').innerHTML = content;
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
    content = '<p>Display errors.</p>';
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
  document.getElementById('aspQuestion').value = JSON.stringify(editor.getValue());
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


