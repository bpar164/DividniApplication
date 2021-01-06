let instructionSections = [];

$(document).ready(() => {
  //For certain pages
  let location = window.location.href;
  if (location.includes('/Assessment/Create')) {
    addInstructionButtonListener();
    let sortable = Sortable.create(questionList);
    createQuestionList(document.getElementById("questionBank").getAttribute("value"));
  }
});

createQuestionList = (questionBank) => {
  let questionDetails = JSON.parse(questionBank);
  for (let i = 0; i < questionDetails.length; i++) {
    let question = questionDetails[i];
    let li = document.createElement('li');
    li.classList.add("collection-item");
    li.id = question.id;
    li.setAttribute('name', question.name);
    li.setAttribute('type', question.type);
    li.innerHTML = createQuestionItemHTML(question);
    document.getElementById("questionList").appendChild(li);
  }
}

createQuestionItemHTML = (question) => {
  return `<div>` + question.name + `<a href="#!" class="secondary-content">
          <label>
            <input type="checkbox" onChange="liCheckBoxChanged('` + question.id + `');" /><span></span>
          </label>
        </a><a href="#previewModal" class="secondary-content modal-trigger tooltipped" data-position="right"
          data-tooltip="Display Contents" onClick="previewQuestion('');"><i
            class="material-icons">pageview</i></a></div>`
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
  return `<div>Instruction Section #` + (id + 1) + `<a href="#!" class="secondary-content">
            <label><input type="checkbox" onChange="liCheckBoxChanged('is` + id + `');" checked /><span></span></label></a>
            <a href="#" class="secondary-content tooltipped" data-position="right" data-tooltip="Edit"
              onClick="editInstructionSection(` + id + `);"><i class="material-icons">edit</i></a>
            <a href="#previewModal" class="secondary-content modal-trigger tooltipped" data-position="right"
              data-tooltip="Details" onClick="previewInstructionSection(` + id + `);"><i class="material-icons">pageview</i></a>
          </div>`;
}

liCheckBoxChanged = (id) => {
  $("#" + id).toggleClass('teal lighten-4');
}

//Fetch instruction section and display in modal
previewInstructionSection = (id) => {
  document.getElementById('previewModalContent').innerHTML = `<p>Details for instruction section ` + (id + 1) + `:</p>` + instructionSections[id];
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
previewExam = () => {
  document.getElementById('generate').classList.remove('disabled');
  document.getElementById('previewModalContent').innerHTML = generateExamPreviewContent();
}

//Creates the HTML content for the preview modal
generateExamPreviewContent = () => {
  //Build the content using the values with formatting
  content = '<p>Review these details before clicking <b>GENERATE</b>:</p>';
  content += '<b>Name: </b>' + document.getElementById('name').value + '</br>';
  document.getElementById('coverPageCheckBox').checked ?
    content += '</br><b>Cover Page:</b></br>' + tinyMCE.get('coverPageTextArea').getContent() + '</br>' : null;

  /* let selectedQuestionIds = fetchAllSelectedQuestionIds();
  let selectedQuestionNames = fetchAllSelectedQuestionNames(selectedQuestionIds);
  content += '</br><b>Questions:</b><ol>';
  selectedQuestionNames.forEach(qName => {
    content += '<li>' + qName + '</li>';
  });
  content += '</ol>'; */
  document.getElementById('appendixCheckBox').checked ?
    content += '</br><b>Appendix:</b></br>' + tinyMCE.get('appendixTextArea').getContent() + '</br>' : null;
  return content;
}

/*
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

$("#examForm").submit((event) => {
  event.preventDefault();
  //Display options modal
  $('#optionsModal').modal('open');
  let missingRequired = false;
  //If cover page or appendix is checked, there must be some content
  let content = '<p>Please fix the following issues:<ul>';
  if (document.getElementById('coverPageCheckBox').checked) {
    if (tinyMCE.get('coverPageTextArea').getContent() === '') {
      missingRequired = true;
      content += '<li>Complete cover page, or uncheck the corresponding box.</li>';
    }
  }
  if (document.getElementById('appendixCheckBox').checked) {
    if (tinyMCE.get('appendixTextArea').getContent() === '') {
      missingRequired = true;
      content += '<li>Complete appendix, or uncheck the corresponding box.</li>';
    }
  }
  //At least one question must be checked
  let selectedQuestionIds = fetchAllSelectedQuestionIds();
  let questionFound = false;
  selectedQuestionIds.forEach(qId => {
    if (qId.length === 24) { //All questions have ids of length 24
      questionFound = true;
    }
  });
  if (!(questionFound)) {
    missingRequired = true;
    content += '<li>Select at least one question.</li>';
  }
  content += '</ul></p>';
  //Show exam type form if all requirements are met, otherwise display message
  if (!(missingRequired)) {
    content = createExamTypeForm();
  } else {
    document.getElementById('optionsModalRetry').classList.remove("disabled");
  }
  document.getElementById('optionsModalContent').innerHTML = content;
});

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

fetchFormValues = () => {
  exam = {};
  exam.name = document.getElementById('name').value;
  exam.paperCount = document.getElementById('paperCount').value;
  exam.examType = examType;
  exam.questionList = createExamQuestionList();
  //The following values can be empty:
  document.getElementById('coverPageCheckBox').checked ? exam.coverPage = tinyMCE.get('coverPageTextArea').getContent() : exam.coverPage = null;
  document.getElementById('appendixCheckBox').checked ? exam.appendix = tinyMCE.get('appendixTextArea').getContent() : exam.appendix = null;
  return exam;
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

