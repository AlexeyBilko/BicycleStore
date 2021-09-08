var tokenKey = "accessToken"

var token;

async function getBicycles() {

    token = sessionStorage.getItem(tokenKey)

    const response = await fetch('/api/bicycles', {
        method: 'GET',
        headers: {
            'Authorization': 'bearer ' + token
        }
    })

    if (response.ok === true) {
        const bicycles = await response.json()
        let rows = document.querySelector('tbody')
        bicycles.forEach(bicycle => rows.append(createTableRow(bicycle)))
    }
}

var bool = false;

function createTableRow(bicycle) {
    const tr = document.createElement('tr')

    const pictureBase64 = document.createElement('td')
    var newImage = document.createElement('img');
    newImage.width = 170;
    newImage.src =bicycle.pictureBase64;
    pictureBase64.append(newImage)
    tr.append(pictureBase64)

    const manufacturerTd = document.createElement('td')
    manufacturerTd.append(bicycle.manufacturer)
    tr.append(manufacturerTd)
    const modelTd = document.createElement('td')
    modelTd.append(bicycle.model)
    tr.append(modelTd)
    const priceTd = document.createElement('td')
    priceTd.append(bicycle.price)
    tr.append(priceTd)
    const weightTd = document.createElement('td')
    weightTd.append(bicycle.weight)
    tr.append(weightTd)
    const weelsradiusTd = document.createElement('td')
    weelsradiusTd.append(bicycle.weelsRadius)
    tr.append(weelsradiusTd)
    const brakesTd = document.createElement('td')
    brakesTd.append(bicycle.brakes)
    tr.append(brakesTd)
    const typeTd = document.createElement('td')
    typeTd.append(bicycle.type)
    tr.append(typeTd)

    const EditBtn = document.createElement('td')
    var a = document.createElement('a')
    a.innerHTML = "edit"
    a.classList.add('btn')
    a.classList.add('btn-warning')
    a.addEventListener('click', function () {
        const form = document.forms['carForm']
        var picture = document.getElementById('img64');
        picture.src = bicycle.pictureBase64;
        form.elements['bicycleId'].value = bicycle.bicycleId
        form.elements['title'].value = bicycle.manufacturer
        form.elements['model'].value = bicycle.model
        form.elements['price'].value = bicycle.price
        form.elements['weight'].value = bicycle.weight
        form.elements['weelsradius'].value = bicycle.weelsRadius
        form.elements['brakes'].value = bicycle.brakes
        form.elements['type'].value = bicycle.type
        bool = true;
    })

    EditBtn.appendChild(a);
    tr.append(EditBtn)


    const DeleteBtn = document.createElement('td')
    var b = document.createElement('a')
    b.innerHTML = "delete"
    b.classList.add('btn')
    b.classList.add('btn-danger')
    b.addEventListener('click', function () {
        deleteBicycle(bicycle.bicycleId)
    })

    DeleteBtn.appendChild(b);
    tr.append(DeleteBtn)

    return tr
}

function encodeImageFileAsURL(element) {
    var file = element.files[0];
    var reader = new FileReader();
    reader.onloadend = function () {

        var img = document.getElementById('img64');
        img.src = reader.result;
        console.log('RESULT', reader.result)
    }
    reader.readAsDataURL(file);
}

async function deleteBicycle(id) {
    const response = await fetch(`api/bicycles/${id}`, {
        method: 'DELETE',
        headers: {
            'Authorization': 'bearer ' + token
        }
    })

    if (response.ok === true) {
        alert(`Bicycle with id = ${id} has been deleted`)
    }
    else{
        alert('Something went wrong!')
    }
}

async function create_Bicycle(bicycleId, pictureBase64, manufacturer, model, price, weight, weelsRadius, brakes, type) {
    if (!bool) {
        const response = await fetch('api/bicycles', {
            method: 'POST',
            headers: {
                'Accept': 'application/json', 'Content-Type': 'application/json',
                    'Authorization': 'bearer ' + token
            },
            body: JSON.stringify({
                manufacturer,
                model,
                price: parseInt(price),
                weight: parseInt(weight),
                weelsRadius: parseInt(weelsRadius),
                brakes: parseInt(brakes),
                type,
                PictureBase64: pictureBase64
            })
        })
        if (response.ok === true) {
            const car = await response.json()
            document.querySelector('tbody').append(createTableRow(car))
        }
        else {
            IfErrors(response)
        }
    }
    else {
        const response = await fetch('https://localhost:44324/api/bicycles', {
            method: 'PUT',
            headers: { 'Accept': 'application/json', 'Content-Type': 'application/json', 'Authorization': 'bearer ' + token },
            body: JSON.stringify({
                BicycleId: parseInt(bicycleId),
                Manufacturer: manufacturer,
                Model: model,
                Price: parseInt(price),
                Weight: parseInt(weight),
                WeelsRadius: parseInt(weelsRadius),
                Brakes: parseInt(brakes),
                Type: type,
                PictureBase64: pictureBase64
            })
        })
        if (response.ok === true) {
            const car = await response.json()
            bool = false;
        }
        else {
            IfErrors(response);
        }
    }
}

async function IfErrors(response) {
    const errorData = await response.json()
    console.log(errorData)
    console.log(errorData.errors)
    if (errorData) {
        if (errorData.errors) {

            if (errorData.errors["Manufacturer"]) {
                showError(errorData.errors["Manufacturer"])
            }

            if (errorData.errors["Price"]) {
                showError(errorData.errors["Price"])
            }

            if (errorData.errors["Brakes"]) {
                showError(errorData.errors["Brakes"])
            }

            if (errorData.errors["WeelsRadius"]) {
                showError(errorData.errors["WeelsRadius"])
            }

            if (errorData.errors["Weight"]) {
                showError(errorData.errors["Weight"])
            }

        }

        if (errorData["Manufacturer"]) {
            showError(errorData["Manufacturer"])
            console.log(errorData["Manufacturer"])
        }
        if (errorData["Price"]) {
            showError(errorData["Price"])
            console.log(errorData["Price"])
        }
        if (errorData["Brakes"]) {
            showError(errorData["Brakes"])
            console.log(errorData["Brakes"])
        }
        if (errorData["WeelsRadius"]) {
            showError(errorData["WeelsRadius"])
            console.log(errorData["WeelsRadius"])
        }
        if (errorData["Weight"]) {
            showError(errorData["Weight"])
            console.log(errorData["Weight"])
        }

        document.getElementById('errors').style.display = 'block'
    }
}

function showError(errors) {
    errors.forEach(error => {
        const p = document.createElement('p')
        p.append(error)
        document.getElementById('errors').append(p)
    })
}

document.forms['carForm'].addEventListener('submit', function (e) {
    e.preventDefault()
    const form = document.forms['carForm']
    const id = form.elements['bicycleId'].value
    var image = document.getElementById('img64')
    var picture = image.src
    const manufacturer = form.elements['title'].value
    const model = form.elements['model'].value
    const price = form.elements['price'].value
    const weight = form.elements['weight'].value
    const weelsradius = form.elements['weelsradius'].value
    const brakes = form.elements['brakes'].value
    const type = form.elements['type'].value

    create_Bicycle(id, picture, manufacturer, model, price, weight, weelsradius, brakes, type)
})

async function getTokenAsync() {
    const credentials = {
        login: document.querySelector('#login').value,
        password: document.querySelector('#password').value
    }

    const response = await fetch('api/Account/token', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(credentials)
    })

    const data = await response.json()
    if (response.ok === true) {
        sessionStorage.setItem(tokenKey, data.access_token)
        getBicycles()   
    } else {
        console.log("Error")
    }


}

document.getElementById('submitLogin').addEventListener('click', function () {
    getTokenAsync()
})

getBicycles()

