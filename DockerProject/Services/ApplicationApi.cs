using System.Collections.Generic;
using DockerProject.Constants;
using DockerProject.Models;
using RestSharp;

namespace DockerProject.Services
{
    public static class ApplicationApi
    {
        public static RestResponse GetToken(string variantNumber)
        {
            return ApiUtil.SendPostRequest(
                $"{Constant.BaseApiUrl}{Endpoints.GetToken}{Constant.Variant}{variantNumber}");
        }

        public static RestResponse<IList<Root>> GetAllTests(string projectId)
        {
            return ApiUtil.SendPostRequest<IList<Root>>(
                $"{Constant.BaseApiUrl}{Endpoints.GetTests}{Constant.ProjectId}{projectId}");
        }

        public static RestResponse AddNewTest(NewTestModel newTestModel)
        {
            return ApiUtil.SendPostRequest(
                $"{Constant.BaseApiUrl}{Endpoints.PostTest}", newTestModel);
        }

        public static RestResponse AddLog(string testId, string content)
        {
            return ApiUtil.SendPostRequest($"{Constant.BaseApiUrl}{Endpoints.PostTest}{Endpoints.PostLog}" +
                                           $"{Constant.TestId}{testId}&{Constant.Content}{content}");
        }

        public static RestResponse AddAttachment(string testId, string content, string contentType)
        {
            return ApiUtil.SendPostRequest($"{Constant.BaseApiUrl}{Endpoints.PostTest}{Endpoints.PostAttachment}" +
                                           $"{Constant.TestId}{testId}&{Constant.ContentType}{contentType}&{Constant.Content}{content}");
        }
    }
}