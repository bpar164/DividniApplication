let correctCount = 0;
let incorrectCount = 0;
let currentType;
const minCorrectTruth = 1;
const minIncorrectTruth = 4;
const minCorrectXYZ = 3;
const minIncorrectXYZ = 3;
const limit = 25;

$(document).ready(() => {
  //Create questionText editor
  tinymce.init({
    selector: '.tinymce-description',
    height: '15em',
    menubar: '',
    toolbar: 'undo redo | styleselect | bold italic underline strikethrough superscript subscript removeformat | bullist numlist table | ',
    plugins: ['lists table']
  });
  //For certain pages
  let location = window.location.href;
  if (location.includes('/Simple/Details') || location.includes('/Simple/Delete') || location.includes('/Simple/Share')) {
    displayQuestionHTML();
  } else if (location.includes('/Simple/Edit') || location.includes('/Simple/Template')) {
    setTimeout(() => { populateQuestionForm(); }, 500); //Give tinyMCE time to load
  }
  if (location.includes('/Simple/Share')) {
    M.toast({ html: document.getElementById("message").getAttribute("Value") });
  }
});

//Set the inner html values for question text and answers
displayQuestionHTML = () => {
  let questionText = document.getElementById('questionText').getAttribute('value');
  document.getElementById('questionText').innerHTML = questionText;
  appendJSONElement(document.getElementById('correct'));
  appendJSONElement(document.getElementById('incorrect'));
}

//Fetches JSON from value attribute of parent and appends each element as a new li
appendJSONElement = (parentElement) => {
  jsonObject = JSON.parse(parentElement.getAttribute('value'));
  for (let item in jsonObject) {
    let li = document.createElement('li');
    li.innerHTML = jsonObject[item];
    parentElement.appendChild(li);
  }
}

//Fetch values passed from controller and populate the form
populateQuestionForm = () => {
  let type = document.getElementById('type').getAttribute('value');
  if (type === "Truth") {
    document.getElementById('truth').setAttribute("checked", true);
  } else {
    document.getElementById('xyz').setAttribute("checked", true);
  }
  let questionText = document.getElementById('questionText').getAttribute('value');
  tinyMCE.get('questionText').setContent(questionText);
  let correctAnswers = JSON.parse(document.getElementById('correct').getAttribute('value'));
  let incorrectAnswers = JSON.parse(document.getElementById('incorrect').getAttribute('value'));
  createAnswers(type, correctAnswers, incorrectAnswers);
}

//For question slots
loadAnswerEditors = () => {
  tinymce.init({
    selector: '.tinymce-small',
    height: '7.5em',
    menubar: '',
    toolbar: 'undo redo | styleselect | bold italic underline strikethrough superscript subscript removeformat | '
  });
}

/*Creates the answer slots, based on the question types
Transfers existing answers when switching between types*/
createAnswers = (type, correctAnswers, incorrectAnswers) => {
  currentType = type;
  //Enable both add answer and preview buttons
  document.getElementById('addCorrect').classList.remove("disabled");
  document.getElementById('addIncorrect').classList.remove("disabled");
  document.getElementById('preview').classList.remove("disabled");
  //Answers will be empty unless the method is loading an existing question
  if (!(correctAnswers)) {
    //Fetch any existing answers 
    correctAnswers = [];
    for (i = 1; i <= correctCount; i++) {
      ans = tinyMCE.get('correctAnswers[' + i + ']').getContent();
      if (!(ans === '')) { correctAnswers.push(ans) } //Only add non-empty answers to array
    }
    incorrectAnswers = [];
    for (i = 1; i <= incorrectCount; i++) {
      ans = tinyMCE.get('incorrectAnswers[' + i + ']').getContent();
      if (!(ans === '')) { incorrectAnswers.push(ans) } //Only add non-empty answers to array
    }
  }
  //Clear answer divs
  document.getElementById('correct').innerHTML = '';
  document.getElementById('incorrect').innerHTML = '';
  //Reset counts
  correctCount = 0;
  incorrectCount = 0;
  //Add the correct number of correct and incorrect answers for the corresponding type
  if (type === 'Truth') {
    createCorrectAnswers(minCorrectTruth, correctAnswers, true);
    createIncorrectAnswers(minIncorrectTruth, incorrectAnswers, true);
  }
  else if (type === 'Xyz') {
    createCorrectAnswers(minCorrectXYZ, correctAnswers, true);
    createIncorrectAnswers(minIncorrectXYZ, incorrectAnswers, true);
  }
  if (correctAnswers.length >= 1) { //Create slots for any remaining correct answers
    createCorrectAnswers(correctAnswers.length, correctAnswers, false);
  }
  if (incorrectAnswers.length >= 1) { //Create slots for any remaining incorrect answers
    createIncorrectAnswers(incorrectAnswers.length, incorrectAnswers, false);
  }
}

//Helps createAnswer to call addAnswer a specified number of times
createCorrectAnswers = (numAnswers, correctAnswers, required) => {
  for (i = 1; i <= numAnswers; i++) {
    let existingVal = null;
    if (correctAnswers.length >= 1) { //Add any existing answers to the slots
      existingVal = correctAnswers[0];
      correctAnswers.shift(); // Remove the first element
    }
    addAnswer('correct', required, existingVal);
  }
}

//Helps createAnswer to call addAnswer a specified number of times
createIncorrectAnswers = (numAnswers, incorrectAnswers, required) => {
  for (i = 1; i <= numAnswers; i++) {
    let existingVal = null;
    if (incorrectAnswers.length >= 1) { //Add any existing answers to the slots
      existingVal = incorrectAnswers[0];
      incorrectAnswers.shift(); // Remove the first element
    }
    addAnswer('incorrect', required, existingVal);
  }
}

//Creates the actual HTML answer slot
addAnswer = (divName, isRequired, value) => {
  let newdiv = document.createElement('div');
  let newpar = document.createElement('p');
  newdiv.classList.add("input-field");
  newdiv.classList.add("tinymce-small");
  isRequired ? newdiv.classList.add("required-field") : null;
  let identifier;
  if ((correctCount <= limit) && (divName === 'correct')) {
    correctCount++;
    identifier = 'correctAnswers[' + correctCount + ']';
    newdiv.id = identifier;
    newdiv.name = identifier;
    document.getElementById(divName).appendChild(newdiv);
    document.getElementById(divName).appendChild(newpar);
    loadAnswerEditors();
    value ? tinyMCE.get(identifier).setContent(value) : null;
  }
  else if ((incorrectCount <= limit) && (divName === 'incorrect')) {
    incorrectCount++;
    identifier = 'incorrectAnswers[' + incorrectCount + ']';
    newdiv.id = identifier;
    newdiv.name = identifier;
    document.getElementById(divName).appendChild(newdiv);
    document.getElementById(divName).appendChild(newpar);
    loadAnswerEditors();
    value ? tinyMCE.get(identifier).setContent(value) : null;
  }
}

//Calls methods to create and populate the preview window
previewAnswer = () => {
  //Enable the generate button
  document.getElementById('generate').classList.remove("disabled");
  //Call a function to create the question object
  question = fetchQuestionFormValues();
  //Call a function to generate the content
  content = generateQuestionPreviewContent(question);
  //Add the values to the display content
  document.getElementById('previewModalContent').innerHTML = content;
}

//Fetches all values from the form
fetchQuestionFormValues = () => {
  question = {};
  question.name = document.getElementById('name').value;
  question.type = currentType;
  question.marks = document.getElementById('marks').value;
  question.questionText = tinyMCE.get('questionText').getContent();
  question.correctAnswers = [];
  for (i = 1; i <= correctCount; i++) {
    question.correctAnswers.push(tinyMCE.get('correctAnswers[' + i + ']').getContent());
  }
  question.incorrectAnswers = [];
  for (i = 1; i <= incorrectCount; i++) {
    question.incorrectAnswers.push(tinyMCE.get('incorrectAnswers[' + i + ']').getContent());
  }
  return question;
}

//Creates the HTML content for the preview window
generateQuestionPreviewContent = (question) => {
  //Clear the content
  content = '';
  //Build the content using the values with formatting 
  content += '<b>Name: </b>' + question.name + '</br>';
  content += '</br><b>Type: </b>' + question.type + '</br>';
  content += '</br><b>Marks: </b>' + question.marks + '</br>';
  content += '</br><b>Question Text:</b></br>' + question.questionText + '</br>';
  content += '</br><b>Correct Answers:</b><ol>';
  question.correctAnswers.forEach(ans => {
    content += '<li>' + ans + '</li>';
  });
  content += '</ol>';
  content += '<b>Incorrect Answers:</b><ol>';
  question.incorrectAnswers.forEach(ans => {
    content += '<li>' + ans + '</li>';
  });
  content += '</ol>';
  return content;
}

//When form is submitted, check for required fields and generate the status modal
$("#questionForm").submit((event) => {
  event.preventDefault();
  //Reset the answer editors to remove empty fields
  createAnswers(currentType, null, null);
  //Display status modal
  $('#statusModal').modal('open');
  let content = '';
  let generate = false;
  //Check that all required text fields have values
  let requiredFields = document.getElementsByClassName('required-field');
  let missingFieldIDs = [];
  Array.from(requiredFields).forEach(field => {
    if (tinyMCE.get(field.id).getContent() === '') {
      missingFieldIDs.push(field.id);
    }
  });
  if (missingFieldIDs.length > 0) {
    content += '<p>Please complete the following fields:<ul>'
    missingFieldIDs.forEach(id => {
      content += '<li>' + id + '</li>'
    });
    content += '</ul></p>';
  } else { //Check that answers are not duplicated
    let question = fetchQuestionFormValues();
    let duplicates = checkForDuplicateAnswers(question.correctAnswers.concat(question.incorrectAnswers));
    if (duplicates) {
      content = '<p>Please ensure that all answers are unique.</p>'
    } else { //All required fields filled in and not duplicated
      content = '<p>Saving question...</p>';
      generate = true;
    }
  }
  document.getElementById('statusModalContent').innerHTML = content;
  if (generate === true) {
    document.getElementById('statusModalClose').classList.add("disabled");
    generateQuestion();
  }
});

//True if there are duplicates
checkForDuplicateAnswers = (array) => {
  return (new Set(array)).size !== array.length;
}

//Add the question details to asp form and submit the form
generateQuestion = () => {
  let question = fetchQuestionFormValues();
  //Set the values in the hidden form
  document.getElementById('aspName').value = question.name;
  document.getElementById('aspType').value = question.type;
  document.getElementById('aspMarks').value = question.marks;
  document.getElementById('aspQuestionText').value = question.questionText;
  document.getElementById('aspCorrectAnswers').value = JSON.stringify(question.correctAnswers);
  document.getElementById('aspIncorrectAnswers').value = JSON.stringify(question.incorrectAnswers);
  document.getElementById('aspModifiedDate').value = new Date().toISOString().slice(0, 10);
  //Submit the hidden form
  document.getElementById("aspQuestionForm").submit();
}

//Submit hidden form, but first check the user email
shareQuestionForm = (event) => {
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





















