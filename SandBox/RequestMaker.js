const gate = require("axios");

const baseUrl = "http://localhost:5000/api";
const args = process.argv.slice(2);

const requestTypeId = args[0] ? Number(args[0]) : 1;
const userId = args[1] ? Number(args[1]) : 2;

(async () => {
    try {

        let data = {
            requestTypeId: requestTypeId,
            UserAccountIdRequester: userId,
            title: "title",
            description: "description"
        };
        console.log(data);
        console.log("*****************************************");
        const res = await gate.post(`${baseUrl}/request/CreateRequestSandBox`, data);

        console.log(res);
    } catch (error) {
        console.error(error);
    }
})();
