// $('#myModal').click(function () {
//     $('#ModalPopup').modal('show');
// });


// This is the code for calling the modal to popup
var myModal = document.getElementById('myModal'); // only javascript code
myModal.addEventListener('click', function(event)
{
    $("#ModalPopup").modal('show'); // this scope here is jQuery code
});




// adding search functionlity




// Todolist api
var editBtn = document.getElementById('todo-edit');
let tableBody = document.getElementById('todo-table-body')
let addItemName = document.getElementById('todo-task-name');
const uri = 'api/todolistapi';
let todos = []

getItems();

async function getItems()
{
    let response = await fetch(`${uri}/GetItems`);
    let data = await response.json();
    
    await createTableRow(data);
    return data;
}

async function createTableRow(data){

    data.forEach(item =>{
        var tr = document.createElement("tr");
        tr.innerHTML = `<td width="50%" id="table-task-name">${item.taskName}</td>
                        <td class="text-center">
                            <div class="w-75 btn-group" role="group">
                                <a id="todo-edit" class="btn btn-primary mx-2">
                                    <i class="fas fa-edit"></i>
                                </a>
                                <a class="btn btn-danger mx-2">
                                    <i class="fas fa-trash-alt"></i>
                                </a>
                        </div>
                    </td>`

        tableBody.appendChild(tr);
    });
}

async function addItems()
{
    const item = {
        TaskName: addItemName.value.trim(),
        TaskStatus: "Incomplete"
    }

    await fetch(`${uri}/AddItems`, {
        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json' 
        },
        body: JSON.stringify(item)
    })
    .then(response => response.json())
    .then(() => {
        getItems();
        addItemName.value = '';
    })
    .catch(error => console.error('Unable to add items', error));
}


