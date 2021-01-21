let questions = [] //Holds all questions on page, indexed by the same ids as the lis
let currentQuestion = {};

$(document).ready(() => {
    currentQuestion = {};
    //For certain pages
    let location = window.location.href;
    if (location.includes('/QuestionBank')) {
        displayAllQuestionLists();
    }
});

setCurrentQuestion = (id, name, type) => {
    currentQuestion.id = id;
    currentQuestion.name = name;
    currentQuestion.type = type;
}

//Fetch all question list JSON objects and display them in the correct card
displayAllQuestionLists = () => {
    let counter = 0; //Counter used to give each li a unique id (for deletion purposes)
    Array.from(document.getElementsByClassName("questionList")).forEach((element) => {
        let sortable = Sortable.create(element);
        json = JSON.parse(element.getAttribute('value'));
        for (let i = 0; i < json.length; i++) {
            let li = document.createElement('li');
            li.id = counter;
            questions.push(json[i]);
            li.classList.add("collection-item");
            li.innerHTML = createListItemHTML(json[i], counter);
            element.appendChild(li);
            counter++;
        }
    });
}

createListItemHTML = (item, parentID) => {
    return `<div><a href='/` + item.type + `/Details/` + item.id + `' target='_blank'>` + item.name + `</a>
                <a href='#void' onClick="deleteItem('` + parentID + `'); "class='secondary-content tooltipped' data-tooltip='Delete' data-position='right'>
                    <i class='material-icons'>delete</i>
                </a>
            </div>`;
}

//Removes li from a ul
deleteItem = (id) => {
    element = document.getElementById(id);
    element.parentNode.removeChild(element);
}

//Submit the question bank details as a POST request
createQuestionBank = (event) => {
    event.preventDefault();
    let name = event.target.elements.name.value;
    $.ajax({
        url: '/QuestionBank/Create',
        method: 'POST',
        data: { 'Name': name, 'QuestionList': '[]', 'UserEmail': document.getElementById('userEmail').getAttribute('value'), 'ModifiedDate': new Date().toISOString().slice(0, 10) },
        success: (res) => {
            if (res === true) {
                window.location.replace("/QuestionBank");
            } else {
                M.toast({ html: 'Error creating question bank. Please try again.' });
            }
        },
        error: () => {
            M.toast({ html: 'Error creating question bank. Please try again.' });
        }
    });
}

//Display the question name in a modal, and add the POST request in an onClick listener to the 'yes' button
deleteQuestionBank = (id, name) => {
    console.log(id, name);
    document.getElementById('confirmModalContent').innerHTML = `Delete question bank: <b>` + name + `</b> ?`;
    document.getElementById('confirmModalYes').addEventListener("click", () => {
        $.ajax({
            url: '/QuestionBank/Delete',
            method: 'POST',
            data: { 'Id': id, },
            success: (res) => {
                if (res === true) {
                    window.location.replace("/QuestionBank");
                } else {
                    M.toast({ html: 'Error deleting question bank. Please try again.' });
                }
            },
            error: () => {
                M.toast({ html: 'Error deleting question bank. Please try again.' });
            }
        });
    });
}

//Fetch the updated details, validate them, and then send them as a POST request
saveQuestionBank = (id) => {
    //Fetch values
    let name = '';
    let questionList;
    Array.from(document.getElementsByClassName(id)).forEach((element) => {
        if (element.hasAttribute('type')) {
            name = element.value;
        } else {
            questionList = element.getElementsByTagName('li');
        }
    });
    //Check name
    if ((name === '') || (!(/^[a-zA-Z0-9][a-zA-Z0-9 ]*/.test(name)))) {
        M.toast({ html: 'Please fix the question bank name.' });
    } else if (questionList.length <= 0) { //Check that there is at least one question
        M.toast({ html: 'Question bank must have at least one question. Refresh the page to reset.' });
    } else {
        //Create new question list
        let newQuestionList = [];
        Array.from(questionList).forEach((element) => {
            newQuestionList.push(questions[element.id]);
        });
        //Send request
        $.ajax({
            url: '/QuestionBank/Edit',
            method: 'POST',
            data: { 'Id': id, 'Name': name, 'QuestionList': JSON.stringify(newQuestionList), 'UserEmail': document.getElementById('userEmail').getAttribute('value'), 'ModifiedDate': new Date().toISOString().slice(0, 10) },
            success: (res) => {
                if (res === true) {
                    window.location.replace("/QuestionBank");
                } else {
                    M.toast({ html: 'Error editing question bank. Please try again.' });
                }
            },
            error: () => {
                M.toast({ html: 'Error editing question bank. Please try again.' });
            }
        });

    }
}

//Add all the user's question banks as options in the dropdown
populateQuestionBankDropdown = (questionBanks) => {
    for (let i = 0; i < questionBanks.length; i++) {
        let li = document.createElement('li');
        li.innerHTML = createDropdownItem(questionBanks[i]);
        document.getElementById('dropdown').appendChild(li);
    }
    $('.dropdown-trigger').dropdown();
}

createDropdownItem = (questionBank) => {
    return `<a onClick="addToQuestionBank('` + questionBank.Id + `');">` + questionBank.Name + `</a>`;
}

//Send a POST request with the data
addToQuestionBank = (id) => {
    $.ajax({
        url: '/QuestionBank/AddQuestion',
        method: 'POST',
        data: { 'bankId': id, 'id': currentQuestion.id, 'name': currentQuestion.name, 'type': currentQuestion.type, 'value': null },
        success: (res) => {
            if (res === true) {
                M.toast({ html: 'Question added.' });
            } else {
                M.toast({ html: 'Error adding question. Please try again.' });
            }
        },
        error: () => {
            M.toast({ html: 'Error adding question. Please try again.' });
        }
    });
}


