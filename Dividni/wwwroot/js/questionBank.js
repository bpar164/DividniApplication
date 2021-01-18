$(document).ready(() => {
    //For certain pages
    let location = window.location.href;
    if (location.includes('/QuestionBank')) {
        displayAllQuestionLists();
    }
});

//Fetch all question list JSON objects and display them in the correct card
displayAllQuestionLists = () => {
    let counter = 0; //Counter used to give each li a unique id (for deletion purposes)
    Array.from(document.getElementsByClassName("questionList")).forEach((element) => {
        let sortable = Sortable.create(element);
        json = JSON.parse(element.getAttribute('value'));
        for (let i = 0; i < json.length; i++) {
            let li = document.createElement('li');
            li.id = counter;
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
