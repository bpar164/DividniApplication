let instructionSections = [];

$(document).ready(() => {
  //For certain pages
  let location = window.location.href;
  if (location.includes('/Assessment/Details') || location.includes('/Assessment/Delete') || location.includes('/Assessment/Share') || location.includes('/Assessment/Download')) {
    displayAssessmentHTML();
  }
  if (location.includes('/Assessment/Create')) {
    addInstructionButtonListener();
    let sortable = Sortable.create(questionList);
    createQuestionList(document.getElementById('questionBank').getAttribute('value'));
  }
});

//Set the inner html values for cover page/question list/appendix
displayAssessmentHTML = () => {
  let coverPage = document.getElementById('coverPage').getAttribute('value');
  coverPage == "" ? coverPage = 'n/a' : null;
  document.getElementById('coverPage').innerHTML = coverPage;
  displayQuestionList(document.getElementById('questionList'));
  let appendix = document.getElementById('appendix').getAttribute('value');
  appendix == "" ? appendix = 'n/a' : null;
  document.getElementById('appendix').innerHTML = appendix;
}

//Fetches JSON from value attribute of questionList and appends each question as a new li
displayQuestionList = (parentElement) => {
  jsonArray = JSON.parse(parentElement.getAttribute('value'));
  for (let i = 0; i < jsonArray.length; i++) {
    let li = document.createElement('li');
    if (jsonArray[i].type == 'Instruction') {
      li.innerHTML = `<a href='#previewModal' class='modal-trigger' 
                        onClick="previewInstructionSection(` + parseInt(jsonArray[i].id.substring(2)) + `, '` + jsonArray[i].value + `');">` + 
                          jsonArray[i].name + `</a>`;
    } else {
      li.innerHTML = `<a href='/` + jsonArray[i].type + `/Details/` + jsonArray[i].id + `' target='_blank'>` + jsonArray[i].name + `</a>`;
    }
    parentElement.appendChild(li);
  }
}

createQuestionList = (questionBank) => {
  let questionDetails = JSON.parse(questionBank);
  for (let i = 0; i < questionDetails.length; i++) {
    let question = questionDetails[i];
    let li = createHTMLElement('li', question.id, ['collection-item', 'teal', 'lighten-4']);
    li.setAttribute('name', question.name);
    li.setAttribute('type', question.type);
    li.addEventListener("click", () => {
      questionClicked(question.id);
    });
    li.innerHTML = createQuestionItemHTML(question);
    document.getElementById("questionList").appendChild(li);
    $('.tooltipped').tooltip({ enterDelay: 500 });
  }
}

createQuestionItemHTML = (question) => {
  return `<div>` + question.name + `
            <a href='/`+ question.type + `/Details/` + question.id + `' class='secondary-content tooltipped' data-tooltip='Details' data-position='right' target='_blank'>
              <i class='material-icons'>pageview</i>
            </a>
          </div>`;
}

questionClicked = (id) => {
  $("#" + id).toggleClass('teal lighten-4');
}

checkBoxChanged = (checkbox, divName) => {
  checkbox.checked ? createTextArea(divName) : removeTextArea(divName);
}

//Create the HTML for the TinyMCE editor
createTextArea = (divName) => {
  let newdiv = createHTMLElement('div', divName + "TextArea", ['input-field', 'tinymce-description']);
  document.getElementById(divName).appendChild(newdiv);
  loadTextArea();
}

//Create an HTML element and add the required classes
createHTMLElement = (element, id, classList) => {
  let newEle = document.createElement(element);
  newEle.id = id;
  for (let i = 0; i < classList.length; i++) {
    newEle.classList.add(classList[i]);
  }
  return newEle;
}

//Initialize TinyMCE editor
loadTextArea = () => {
  tinymce.init({
    selector: '.tinymce-description',
    height: '30em',
    menubar: '',
    toolbar: 'undo redo | styleselect | bold italic underline strikethrough superscript subscript removeformat | bullist numlist table | ',
    plugins: ['lists table']
  });
}

//Remove textArea from the div
removeTextArea = (divName) => {
  let divID = divName + "TextArea";
  tinymce.remove('#' + divID);
  document.getElementById(divName).removeChild(document.getElementById(divID));
}

//Event listener for btnInstructions to create textArea and additional buttons - used when instruction section is created for first time
addInstructionButtonListener = () => {
  document.getElementById("btnInstructions").addEventListener("click", () => {
    createInstructionTextAreaAndButtons();
    //Add corresponding onClick listeners to buttons
    document.getElementById("btnSave").addEventListener("click", () => {
      //Save instruction section (if there is content)
      if (!(tinyMCE.get('instructionSectionsTextArea').getContent() === '')) {
        let sectionId = (instructionSections.push(tinyMCE.get('instructionSectionsTextArea').getContent()) - 1);
        //Create row to append to question table
        let instructionItem = createHTMLElement('li', 'is' + sectionId, ['collection-item', 'teal', 'lighten-4']);
        instructionItem.setAttribute("name", "Instruction Section #" + (sectionId + 1));
        instructionItem.setAttribute('type', "Instruction");
        instructionItem.addEventListener("click", () => {
          questionClicked('is' + sectionId);
        });
        instructionItem.innerHTML = createInstructionItemHTML(sectionId);
        document.getElementById('questionList').appendChild(instructionItem);
        $('.tooltipped').tooltip({ enterDelay: 500 });
      }
      removeInstructionSection();
    });
    document.getElementById("btnCancel").addEventListener("click", () => {
      removeInstructionSection();
    });
  });
}

createInstructionTextAreaAndButtons = () => {
  //If the textArea already exists, remove it 
  if (document.getElementById("instructionSectionsTextArea") !== null) {
    removeInstructionSection();
  }
  createTextArea('instructionSections');
  document.getElementById("btnInstructions").classList.add('disabled');
  //Create save and cancel buttons
  let btnSave = createHTMLElement('a', 'btnSave', ['waves-effect', 'waves-light', 'btn-small']);
  btnSave.innerHTML = 'Save';
  let btnCancel = createHTMLElement('a', 'btnCancel', ['waves-effect', 'waves-light', 'btn-small', 'right']);
  btnCancel.innerHTML = 'Cancel';
  //Add buttons to display 
  document.getElementById('instructionSections').appendChild(btnSave);
  document.getElementById('instructionSections').appendChild(btnCancel);
}

//Remove the textArea and buttons
removeInstructionSection = () => {
  removeTextArea('instructionSections');
  document.getElementById('instructionSections').removeChild(btnSave);
  document.getElementById('instructionSections').removeChild(btnCancel);
  document.getElementById("btnInstructions").classList.remove('disabled');
}

createInstructionItemHTML = (id) => {
  return `<div>Instruction Section #` + (id + 1) + `
            <a href="#previewModal" class="secondary-content modal-trigger tooltipped" data-position="right" data-tooltip="Details" 
              onClick="previewInstructionSection(` + id + `,'` + instructionSections[id] + `');"><i class="material-icons">pageview</i></a>
            <a href="#" class="secondary-content tooltipped" data-position="right" data-tooltip="Edit"
              onClick="editInstructionSection(` + id + `);"><i class="material-icons">edit</i></a>
          </div>`;
}

//Fetch instruction section and display in modal
previewInstructionSection = (id, contents) => {
  document.getElementById('previewModalContent').innerHTML = `<p>Details for instruction section ` + (id + 1) + `:</p>` + contents;
}

//Make changes to existing instruction section element
editInstructionSection = (id) => {
  createInstructionTextAreaAndButtons();
  //Load contents into textArea
  tinyMCE.get('instructionSectionsTextArea').setContent(instructionSections[id]);
  //Add corresponding onClick listeners to buttons
  document.getElementById("btnSave").addEventListener("click", () => {
    let temp = tinyMCE.get('instructionSectionsTextArea').getContent();
    if (!(temp === '')) {
      instructionSections[id] = temp;
    }
    removeInstructionSection();
  });
  document.getElementById("btnCancel").addEventListener("click", () => {
    removeInstructionSection();
  });
}

//Preview exam and display in modal
previewAssessment = () => {
  document.getElementById('generate').classList.remove('disabled');
  document.getElementById('previewModalContent').innerHTML = generatePreviewContent();
}

//Creates the HTML content for the preview modal
generatePreviewContent = () => {
  //Build the content using the values with formatting
  content = '<p>Review these details before clicking <b>GENERATE</b>:</p>';
  content += '<b>Name: </b>' + document.getElementById('name').value + '</br>';
  document.getElementById('coverPageCheckBox').checked ?
    content += '</br><b>Cover Page:</b></br>' + tinyMCE.get('coverPageTextArea').getContent() + '</br>' : null;

  let selectedQuestionIds = fetchAllSelectedQuestionIds();
  let selectedQuestionNames = fetchAllSelectedQuestionNames(selectedQuestionIds);
  content += '</br><b>Questions:</b><ol>';
  selectedQuestionNames.forEach(qName => {
    content += '<li>' + qName + '</li>';
  });
  content += '</ol>'; 
  document.getElementById('appendixCheckBox').checked ?
    content += '</br><b>Appendix:</b></br>' + tinyMCE.get('appendixTextArea').getContent() + '</br>' : null;
  return content;
}

fetchAllSelectedQuestionIds = () => {
  let selectedQuestions = document.querySelectorAll('#questions>ul>li.teal');
  let selectedQuestionIds = [];
  for (let i = 0; i < selectedQuestions.length; i++) {
    selectedQuestionIds.push(selectedQuestions[i].id);
  }
  return selectedQuestionIds;
}

fetchAllSelectedQuestionNames = (selectedQuestionIds) => {
  let selectedQuestionNames = [];
  for (let i = 0; i < selectedQuestionIds.length; i++) {
    selectedQuestionNames.push(document.getElementById(selectedQuestionIds[i]).getAttribute('name'));
  }
  return selectedQuestionNames;
}

fetchAllSelectedQuestionTypes = (selectedQuestionIds) => {
  let selectedQuestionTypes = [];
  for (let i = 0; i < selectedQuestionIds.length; i++) {
    selectedQuestionTypes.push(document.getElementById(selectedQuestionIds[i]).getAttribute('type'));
  }
  return selectedQuestionTypes;
}

$("#assessmentForm").submit((event) => {
  event.preventDefault();
  //Display status modal
  $('#statusModal').modal('open');
  let missingRequired = false;
  //If cover page or appendix is checked, there must be some content
  let content = '<p>Please fix the following issues:<ul>';
  if (document.getElementById('coverPageCheckBox').checked) {
    if (tinyMCE.get('coverPageTextArea').getContent() === '') {
      missingRequired = true;
      console.log('cp')
      content += '<li>Complete cover page, or uncheck the corresponding box.</li>';
    }
  }
  if (document.getElementById('appendixCheckBox').checked) {
    if (tinyMCE.get('appendixTextArea').getContent() === '') {
      missingRequired = true;
      content += '<li>Complete appendix, or uncheck the corresponding box.</li>';
    }
  }
  //At least one simple or advanced question must be included
  let selectedQuestionIds = fetchAllSelectedQuestionIds();
  let selectedQuestionTypes = fetchAllSelectedQuestionTypes(selectedQuestionIds);
  if (!(selectedQuestionTypes.includes("Simple") || selectedQuestionTypes.includes("Advanced"))) {
    missingRequired = true;
    content += '<li>Include at least one question.</li>';
  }
  content += '</ul></p>';
  //Generate assessment if all requirements are met, otherwise display message
  if (!(missingRequired)) {
    generateAssessment();
    content = '<p>Saving assessment...</p>';
  } 
  document.getElementById('statusModalContent').innerHTML = content;
});

generateAssessment = () => {
  let assessment = fetchFormValues();
  //Set the values in the hidden form
  document.getElementById('aspName').value = assessment.name;
  document.getElementById('aspCoverPage').value = assessment.coverPage;
  document.getElementById('aspQuestionList').value = JSON.stringify(assessment.questionList);
  document.getElementById('aspAppendix').value = assessment.appendix;
  document.getElementById('aspModifiedDate').value = new Date().toISOString().slice(0, 10);
  //Submit the hidden form
  document.getElementById("aspAssessmentForm").submit();
}

fetchFormValues = () => {
  assessment = {};
  assessment.name = document.getElementById('name').value;
  //Question List
  let questionList = [];
  let selectedQuestionIds = fetchAllSelectedQuestionIds();
  let selectedQuestionNames = fetchAllSelectedQuestionNames(selectedQuestionIds);
  let selectedQuestionTypes = fetchAllSelectedQuestionTypes(selectedQuestionIds);
  for (let i = 0; i < selectedQuestionIds.length; i++) {
    let question = {};
    question.id = selectedQuestionIds[i];
    question.name = selectedQuestionNames[i];
    question.type = selectedQuestionTypes[i];
    question.type == 'Instruction' ? question.value = instructionSections[question.id.substring(2)] : question.value = null; 
    questionList.push(question);
  }
  assessment.questionList = questionList;
  //The following values can be empty:
  document.getElementById('coverPageCheckBox').checked ? assessment.coverPage = tinyMCE.get('coverPageTextArea').getContent() : assessment.coverPage = null;
  document.getElementById('appendixCheckBox').checked ? assessment.appendix = tinyMCE.get('appendixTextArea').getContent() : assessment.appendix = null;
  return assessment;
}

//Submit hidden form, but first check the user email
shareAssessmentForm = (event) => {
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
    document.getElementById("aspAssessmentForm").submit();
  }
}

/*
createExamTypeForm = () => {
  return `<div class="input-field">
      <p>Exam type:</p>
      <p><label>
          <input class="with-gap" type="radio" name="type" value="standard" required" onClick="setExamType('standard');" checked>
          <span>Standard</span>
      </label></p>
      <p><label>
          <input class="with-gap" type="radio" name="type" value="canvas" required" onClick="setExamType('canvas');">
          <span>Canvas</span>
      </label></p>
      <p><label>
          <input class="with-gap" type="radio" name="type" value="moodle" required" onClick="setExamType('moodle');">
          <span>Moodle</span>
      </label></p>
      <p><label>
          <input class="with-gap" type="radio" name="type" value="inspera" required" onClick="setExamType('inspera');">
          <span>Inspera</span>
      </label></p>
    </div>
    <button class="btn waves-effect waves-light" onClick="generateExam('');">
        Select<i class="material-icons right">send</i>
    </button>`
}

setExamType = (type) => {
  examType = type;
}

generateExam = () => {
  document.getElementById('optionsModalRetry').classList.add("disabled");
  document.getElementById('optionsModalContent').innerHTML = '<p>Generating exam...</p>';
  //Check what action is being performed on the exam
  let examMode = document.getElementById('examMode');
  if ((examMode) && (examMode.getAttribute('data-exam-action') === 'EDIT')) {
    updateExam(examMode.getAttribute('data-exam-id')); //Edit exam
  } else {
    generateNewExam(); //Create new exam (from scratch or from template)
  }
}

generateNewExam = () => {
  //Get the values from the form
  let exam = fetchFormValues();
  $.ajax({
    url: 'exams/currentUserID',
    method: 'POST',
    data: exam,
    success: (res) => {
      if (res === 'true') {
        //Exam generated
        document.getElementById('optionsModalContent').innerHTML = '<p>Exam generated.</p>';
        if ((exam.paperCount > 1) && (examType === 'standard')) {
          document.getElementById('optionsModalContent').innerHTML +=
            `<div class=row><button class="btn waves-effect waves-light" id="mergePDFs" onClick="mergePDFs('` + exam.name + `');">Merge PDFs?</button></div>`;
        }
        document.getElementById('optionsModalContent').innerHTML +=
          `<div class=row><a class="waves-effect waves-light btn" href="exams/download/` + exam.name + `" onClick="downloadExam();">Download Exam</a></div>`
      } else if (res === 'false') {
        //Exam not generated
        document.getElementById('optionsModalContent').innerHTML = '<p>Error generating exam.</p>';
        enableAllOptionsButtons();
      }
    },
    error: () => {
      document.getElementById('optionsModalContent').innerHTML = '<p>Error generating exam.</p>';
      enableAllOptionsButtons();
    }
  });
}

//Make the actual request to update the exam in the database
updateExam = (id) => {
  document.getElementById('optionsModalContent').innerHTML = '<p>Editing exam...</p>';
  //Get the values from the form
  let exam = fetchFormValues();
  $.ajax({
    url: 'exam/' + id,
    method: 'PUT',
    data: exam,
    success: (res) => {
      if (res === 'true') {
        //Exam updated
        document.getElementById('optionsModalContent').innerHTML = '<p>Exam edited.</p>';
        enableAllOptionsButtons();
        document.getElementById('optionsModalRetry').classList.add("disabled");
      } else if (res === 'false') {
        //Exam not generated
        document.getElementById('optionsModalContent').innerHTML = '<p>Error generating exam.</p>';
        enableAllOptionsButtons();
      }
    },
    error: () => {
      document.getElementById('optionsModalContent').innerHTML = '<p>Error generating exam.</p>';
      enableAllOptionsButtons();
    }
  });
}

enableAllOptionsButtons = () => {
  document.getElementById('optionsModalRetry').classList.remove("disabled");
  document.getElementById('optionsModalCreate').classList.remove("disabled");
  document.getElementById('optionsModalView').classList.remove("disabled");
}

createExamQuestionList = () => {
  let selectedQuestionIds = fetchAllSelectedQuestionIds();
  let selectedQuestionNames = fetchAllSelectedQuestionNames(selectedQuestionIds);
  let questionList = [];
  let instructionSectionsIndex = 0;
  for (let i = 0; i < selectedQuestionIds.length; i++) {
    let tempQuestion = {};
    tempQuestion.id = selectedQuestionIds[i];
    tempQuestion.name = selectedQuestionNames[i];
    if (selectedQuestionIds[i].length === 24) { //All questions have ids of length 24
      tempQuestion.type = 'mc';
      tempQuestion.contents = null;
    } else {
      tempQuestion.type = 'is';
      tempQuestion.contents = instructionSections[instructionSectionsIndex];
      instructionSectionsIndex++;
    }
    questionList.push(tempQuestion);
  }
  return questionList;
}

mergePDFs = (examName) => {
  $.ajax({
    url: 'exams/merge/' + examName,
    method: 'GET',
    dataType: 'json'
  });
  document.getElementById('mergePDFs').classList.add('disabled');
}

downloadExam = () => {
  document.getElementById('optionsModalContent').innerHTML = '<p>Exam download will begin shortly.</p>';
  document.getElementById('optionsModalCreate').classList.remove("disabled");
  document.getElementById('optionsModalView').classList.remove("disabled");
}

populateExamForm = (id) => {
  //Fetch exam
  $.ajax({
    url: 'exams-my/' + id,
    method: 'GET',
    dataType: 'json',
    success: (res) => {
      if (!(res.exam)) {
        //Display message if no exam found
        $('#previewModal').modal();
        document.getElementById('previewModalContent').innerHTML = '<p>Error loading exam.</p>';
      } else {
        //Populate form
        document.getElementById('name').value = res.exam.name;
        document.getElementById('paperCount').value = res.exam.paperCount;
        if (res.exam.coverPage !== "") {
          document.getElementById("coverPageCheckBox").setAttribute("checked", true);
          createTextArea('coverPage');
          setTimeout(() => { tinyMCE.get('coverPageTextArea').setContent(res.exam.coverPage); }, 500); //Give tinyMCE time to load
        }
        if (res.exam.appendix !== "") {
          document.getElementById("appendixCheckBox").setAttribute("checked", true);
          createTextArea('appendix');
          setTimeout(() => { tinyMCE.get('appendixTextArea').setContent(res.exam.appendix); }, 500); //Give tinyMCE time to load
        }
        populateExamQuestionList(res.exam.questionList);
      }
    },
    error: () => {
      $('#previewModal').modal();
      document.getElementById('previewModalContent').innerHTML = '<p>Error loading exam.</p>';
    }
  });
}

populateExamQuestionList = (questionList) => {
  //First check that question ul exists and create it if necessary
  if (document.getElementById('questionList') === null) {
    //Create ul
    let ul = createHTMLElement('ul', 'questionList', ['collection', 'with-header']);
    document.getElementById('questions').appendChild(ul);
  }
  //Add questions to existing list
  let examQuestionIDs = [];
  for (let i = 0; i < questionList.length; i++) {
    //Create instruction li items to append to display list
    let item = createHTMLElement('li', questionList[i].id, ['collection-item', 'teal', 'lighten-4']);
    item.setAttribute("name", questionList[i].name);
    if (questionList[i].type === 'is') {
      instructionSections.push(questionList[i].contents)
      item.innerHTML = createInstructionItemHTML(i);
    } else {
      item.innerHTML = createQuestionItemHTML(questionList[i].id, questionList[i].name);
      examQuestionIDs.push(questionList[i].id);
    }
    document.getElementById('questionList').appendChild(item);
    $('.tooltipped').tooltip({ enterDelay: 500 });
  }
  //Remove duplicate lis
  let questions = document.querySelectorAll('#questions>ul>li');
  for (let i = 0; i < questions.length; i++) {
    if (questions[i].classList.length === 1) { //Unselected questions have only one class
      if (examQuestionIDs.includes(questions[i].id)) {
        let li = document.getElementById(questions[i].id);
        document.getElementById('questionList').removeChild(li);
      }
    }
  }
}

downloadExistingExam = (id) => {
  //Bring up the display window
  $('#optionsModal').modal('open');
  document.getElementById('optionsModalContent').innerHTML = '<p>Generating exam files...</p>';
  $.ajax({
    url: 'exams-my/download/' + id,
    method: 'GET',
    success: (res) => {
      if (res.exam) {
        //Exam fetched and generated
        document.getElementById('optionsModalContent').innerHTML = '<p>Exam files generated.</p>';
        if ((res.exam.paperCount > 1) && (res.exam.examType === 'standard')) {
          document.getElementById('optionsModalContent').innerHTML +=
            `<div class=row><button class="btn waves-effect waves-light" id="mergePDFs" onClick="mergePDFs('` + res.exam.name + `');">Merge PDFs?</button></div>`;
        }
        document.getElementById('optionsModalContent').innerHTML +=
          `<div class=row><a class="waves-effect waves-light btn" href="exams/download/` + res.exam.name + `" onClick="downloadExam();">Download Exam</a></div>`
      } else {
        //Exam not generated
        document.getElementById('optionsModalContent').innerHTML = '<p>Error generating exam.</p>';
        enableAllOptionsButtons();
        document.getElementById('optionsModalRetry').classList.add("disabled");
      }
    },
    error: () => {
      document.getElementById('optionsModalContent').innerHTML = '<p>Error generating exam.</p>';
      enableAllOptionsButtons();
      document.getElementById('optionsModalRetry').classList.add("disabled");
    }
  });
} */

