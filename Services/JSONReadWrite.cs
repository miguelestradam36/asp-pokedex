namespace ASP_Pokemon.Services
{
    public class JSONReadWrite
    {
        public JSONReadWrite() { }

        public string Read(string fileName, string location)
        {
            string root = "wwwroot";
            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                root,
                location,
                fileName);

            string jsonResult;

            using (StreamReader streamReader = new StreamReader(path))
            {
                jsonResult = streamReader.ReadToEnd();
            }
            return jsonResult;
        }
        public void Write(string fileName, string location, string jSONString)
        {
            string root = "wwwroot";
            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                root,
                location,
                fileName);

            using (var streamWriter = File.CreateText(path))
            {
                streamWriter.Write(jSONString);
            }
        }
    }
}
