const gate = require("axios");

const route = "http://localhost:5000/testmail/test";
const args = process.argv.slice(2);
const yourJWTToken =
    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjE2Iiwicm9sZSI6IlJlYWxFc3RhdGVBZG1pbmlzdHJhdG9yIiwiRnVsbE5hbWUiOiJtaWxhZCAgYm9uYWtkYXIiLCJlbWFpbCI6IlJlYWxFc3RhdGVBZG1pbmlzdHJhdG9yQGdtYWlsLmNvbSIsIlppcENvZGUiOiIiLCJBZGRyZXNzIjoiIiwiUGhvbmUiOiIiLCJHcm91cElkIjoiMiIsIlJlZ2lzdHJhdGlvbkRhdGUiOiIwOC8yNi8yMDE5IDEyOjM4OjMzIiwiTGFuZ3VhZ2VJZCI6IjEiLCJBZ2VudElkIjoiNiIsIklzUmVzcG9uc2libGUiOiJGYWxzZSIsIlJlYWxFc3RhdGVJZCI6IjUiLCJuYmYiOjE1NjcxODI5NzAsImV4cCI6MTU2Nzc4Nzc3MCwiaWF0IjoxNTY3MTgyOTcwfQ.OtdbbMZ7VnpWMFC_yMEK8WQcUUBpflFcnzWic1g1mbU";

(async () => {
    try {
        const res = await gate.get(route, {
            headers: {
                Authorization: "Bearer " + yourJWTToken
            }
        });
        console.log(res.data);
    } catch (error) {
        console.error(error);
    }
})();
