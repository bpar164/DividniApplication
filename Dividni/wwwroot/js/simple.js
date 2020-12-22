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
});

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
  question = fetchFormValues();
  //Call a function to generate the content
  content = generatePreviewContent(question);
  //Add the values to the display content
  document.getElementById('previewModalContent').innerHTML = content;
}

//Fetches all values from the form
fetchFormValues = () => {
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
generatePreviewContent = (question) => {
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
  //Display status modal
  $('#statusModal').modal('open');
  //Check that all required text fields have values
  let requiredFields = document.getElementsByClassName('required-field');
  let missingFieldIDs = [];
  let missingRequired = false;
  Array.from(requiredFields).forEach(field => {
    if (tinyMCE.get(field.id).getContent() === '') {
      missingRequired = true;
      missingFieldIDs.push(field.id);
    }
  });

  //Run in while loop so that required checked and only move on to next 

  //Check that answers are not duplicated
  let question = fetchFormValues();
  let dupCorrects = checkForDuplicateAnswers(question.correctAnswers);
  let dupIncorrects = checkForDuplicateAnswers(question.incorrectAnswers);
  if ((missingRequired) || (dupCorrects) || (dupIncorrects)) {
    let content = '';
    if (missingRequired) {
      content += '<p>Please complete the following fields:<ul>'
      missingFieldIDs.forEach(id => {
        content += '<li>' + id + '</li>'
      });
      content += '</ul></p>';
    } 
    if ((dupCorrects) || (dupIncorrects)) {
      content += '<p>Please ensure that all answers are unique.</p>'
    }
    document.getElementById('statusModalContent').innerHTML = content;
  } else { //All required fields filled in
    generateQuestion(); 
  }
});

//True if there are duplicates
checkForDuplicateAnswers = (array) => {
  //Trim off empty answer fields (all required fields are filled in at this point)

  return (new Set(array)).size !== array.length;
  
  
}

//Add the question details to asp form and submit the form
generateQuestion = () => {
  document.getElementById('statusModalContent').innerHTML = '<p>Generating question...</p>';
  document.getElementById('statusModalClose').classList.add("disabled");
  //Get the values from the form
  let question = fetchFormValues();
  //Set the values in the hidden form
  document.getElementById('aspName').value = question.name;
  document.getElementById('aspType').value = question.type;
  document.getElementById('aspMarks').value = question.marks;
  document.getElementById('aspQuestionText').value = question.questionText;
  document.getElementById('aspCorrectAnswers').value = JSON.stringify(question.correctAnswers);
  document.getElementById('aspIncorrectAnswers').value = JSON.stringify(question.incorrectAnswers);
  //Submit the hidden form
  document.getElementById("aspQuestionForm").submit();
}

populateQuestionForm = (id) => {
  //Fetch question
  /* $.ajax({
    url: 'multiple-choice-my/' + id,
    method: 'GET',
    dataType: 'json',
    success: (res) => {
      if (!(res.question)) {
        //Display message if no question found
        $('#previewModal').modal();
        document.getElementById('previewModalContent').innerHTML = '<p>Error loading question.</p>';
      } else {
        //Populate form
        document.getElementById('name').value = res.question.name;
        if (res.question.type === "Truth") {
          document.getElementById('truth').setAttribute("checked", true);
        } else {
          document.getElementById('xyz').setAttribute("checked", true);
        }
        document.getElementById('marks').value = res.question.marks;
        let correctAnswers = res.question.correctAnswers;
        let incorrectAnswers = res.question.incorrectAnswers;
        createAnswers(res.question.type, correctAnswers, incorrectAnswers);
        tinyMCE.get('questionText').setContent(res.question.questionText);
      }
    },
    error: () => {
      $('#previewModal').modal();
      document.getElementById('previewModalContent').innerHTML = '<p>Error loading question.</p>';
    }
  }); */
}













