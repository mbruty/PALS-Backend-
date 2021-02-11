// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const url = "https://localhost:44350/api/data";

window.onload = () => {
    // init materialize tabs
    M.Tabs.init(document.querySelector(".tabs"));

    console.log( window.localStorage.getItem("1") );

    fetchData();

    const saveBtn = document.getElementById("save");
    saveBtn.addEventListener("click", (e) => {
        // This will prevent the page from refreshing when you click submit
        e.preventDefault();

        postData()
            .then(function () {
            document.querySelector("form").reset();
            M.toast({ html: 'Saved!', classes: 'rounded' });
            fetchData();
        }).catch(function () {
            M.toast({ html: 'There was an error saving', classes: 'rounded' });
        })
    })
};

async function fetchData() {
    const raw = await fetch(url);
    const data = await raw.json();
    console.table(data);
    const table = document.createElement("table");
    table.innerHTML = "<tr><th>First name</th><th>Last Name</th><th>delete</td></tr>";
    data.forEach(({id, firstName, lastName }) => {
        const row = document.createElement("tr");
        row.innerHTML = `<td>${firstName}</td><td>${lastName}</td><td><a class="waves-effect waves-light btn" onclick="deleteId(${id})"><i class="material-icons left">delete</i>delete</a></td>`;
        table.appendChild(row);
    })
    const div = document.getElementById("test1");
    div.replaceChild(table, div.childNodes[0]);
}

async function postData() {
    const form = document.querySelector("form");
    const fname = form.elements["first_name"].value;
    const lname = form.elements["last_name"].value;
    console.log({ fname, lname });
    try {
        await fetch(url, {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(
                {
                    firstName: fname,
                    lastName: lname
                }
            )
        });
    } catch (e) {
        throw "Failed to post";
    }
    return;
}

async function deleteId(id) {
    try {
        await fetch(url + "/" + id , {
            method: "DELETE",
        });
        M.toast({ html: 'Deleted!', classes: 'rounded' });
        fetchData();
    } catch (e) {
        M.toast({ html: 'Faield to save 😢', classes: 'rounded' });
    }
}