A default user with email 'admin@admin.com' and password 'admin1234A.' is automatically seeded to the CosmosDB in order to log in and have acces to administrator endpoints.

Login API:
endpoint: /api/Account/Login
method: POST
payload: {
  "userName": "string",
  "email": "string",
  "password": "string"
}

For screen 1, admin needs to login

To create new application forms
endpoint: /api/ProgramForm/Create
method: POST
payload: {
  "programTitle": "string",
  "programDescription": "string",
  "sections": [
    {
      "title": "string",
      "questions": [
        {
          "type": 0,
          "question": "string",
          "choices": [
            "string"
          ],
          "isOtherOptionEnabled": true,
          "maxChoiceAllowed": 0,
          "isMandatory": true,
          "isInternal": true,
          "isHidden": true
        }
      ]
    }
  ]
}

To update the application forms
endpoint: /api/ProgramForm/Update
method: PUT
payload: {
  "programTitle": "string",
  "programDescription": "string",
  "sections": [
    {
      "title": "string",
      "questions": [
        {
          "type": 0,
          "question": "string",
          "choices": [
            "string"
          ],
          "isOtherOptionEnabled": true,
          "maxChoiceAllowed": 0,
          "isMandatory": true,
          "isInternal": true,
          "isHidden": true
        }
      ]
    }
  ],
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}

To get the different question types, the description and the vaules
endpoint: /api/Enums/GetQuestionType
method: GET
response sample: {
  "succeeded": true,
  "result": [
    {
      "value": 0,
      "name": "Paragraph",
      "description": "Paragraph"
    },
    {
      "value": 1,
      "name": "YesNo",
      "description": "Yes/No"
    },
    {
      "value": 2,
      "name": "Dropdown",
      "description": "Dropdown"
    },
    {
      "value": 3,
      "name": "MultipleChoice",
      "description": "Multiple Choice"
    },
    {
      "value": 4,
      "name": "Date",
      "description": "Date"
    },
    {
      "value": 5,
      "name": "Number",
      "description": "Number"
    }
  ],
  "exceptionError": null,
  "message": null
}


For Screen 2:

To render the form for users to use
endpoint: /api/ProgramForm/GetById?id={formid}
method: GET
response sample: {
  "succeeded": true,
  "result": {
    "programTitle": "string",
    "programDescription": "string",
    "sections": [
      {
        "title": "string",
        "questions": [
          {
            "type": 0,
            "question": "string",
            "choices": [
              "string"
            ],
            "isOtherOptionEnabled": true,
            "maxChoiceAllowed": 0,
            "isMandatory": true,
            "isInternal": true,
            "isHidden": true
          }
        ]
      }
    ],
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
  },
  "exceptionError": "string",
  "message": "string"
}

To post candidate response to appliction:
endpoint: /api/FormResponses/Create
method: POST
payload: {
  "programFormId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "questionAnswers": [
    {
      "question": "string",
      "questionType": 0,
      "isMandatory": true,
      "answer": "string"
    }
  ]
}

