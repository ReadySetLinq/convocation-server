﻿using System;
using Newtonsoft.Json.Linq;
using ConvocationServer.Extensions;
using ConvocationServer.Websockets;

namespace ConvocationServer.XPN
{
    class XPN_Events
    {
        public static bool VerifyProperties(WebSocketSession session, JObject serviceProps, string[] properties, bool invalidResponse = true)
        {
            bool wasVerified = true;
            string _message = "missing property: ";
            foreach (string property in properties)
            {
                if (!serviceProps.ContainsKey(property))
                {
                    _message += property + ", ";
                    wasVerified = false;
                }
            }

            if (!wasVerified && invalidResponse)
            {
                session.SendMessage(message: new JObject
                    {
                        { "service", "xpression" },
                        { "data", new JObject {
                            { "category", "main" },
                            { "action", "error" },
                            { "value", new JObject {
                                { "uuid", serviceProps["uuid"]?.ToString() },
                                { "response", _message.ReplaceLastOccurrence(", ", string.Empty) + "!"}
                            }},
                        }}
                    });
            }
            return wasVerified;
        }

        public static void Execute(FrmServer parent, XPN_Functions xpn, WebSocketSession session, XpressionService data)
        {
            try
            {
                // Make sure a category and action was given
                if (!string.IsNullOrEmpty(data.Category) && !string.IsNullOrEmpty(data.Action))
                {
                    // Do stuff with Xpression here
                    parent.AddMessage(JObject.FromObject(data), $"{data.Action.FirstCharToUpper()} request", "Incoming");

                    // Make sure expression is running or the start command is being given, if not send error
                    if (xpn == null && (!data.Category.Equals("main", StringComparison.OrdinalIgnoreCase) && !data.Action.Equals("start", StringComparison.OrdinalIgnoreCase)))
                    {
                        session.SendXpressionResponse(category: "main",
                                                     action: "error",
                                                     value: new JObject {
                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                        { "response", "Xpression must first be started!"}
                                                    });
                        return;
                    }

                    switch (data.Category.ToLower())
                    {
                        case "main":

                            if (data.Action == "start")
                            {
                                session.SendXpressionResponse(category: "main",
                                                             action: "start",
                                                             value: new JObject {
                                                                { "uuid", data.Properties["uuid"]?.ToString() },
                                                                { "response", xpn.Start()}
                                                            });
                            }
                            else if (data.Action == "stop")
                            {
                                session.SendXpressionResponse(category: "main",
                                                             action: "stop",
                                                             value: new JObject {
                                                                { "uuid", data.Properties["uuid"]?.ToString() },
                                                                { "response", xpn.Stop()}
                                                            });
                            }

                            break;

                        case "project":

                            if (data.Action.Equals("ProjectPath", StringComparison.OrdinalIgnoreCase))
                            {
                                session.SendXpressionResponse(category: "project",
                                                             action: "ProjectPath",
                                                             value: new JObject {
                                                                { "uuid", data.Properties["uuid"]?.ToString() },
                                                                { "response", xpn.ProjectPath()}
                                                            });
                            }
                            else if (data.Action.Equals("ProjectFileName", StringComparison.OrdinalIgnoreCase))
                            {
                                session.SendXpressionResponse(category: "project",
                                                             action: "ProjectFileName",
                                                             value: new JObject {
                                                                { "uuid", data.Properties["uuid"]?.ToString() },
                                                                { "response", xpn.ProjectFileName()}
                                                            });
                            }
                            else if (data.Action.Equals("CloseProject", StringComparison.OrdinalIgnoreCase))
                            {
                                xpn.CloseProject();
                                session.SendXpressionResponse(category: "project",
                                                             action: "CloseProject",
                                                             value: new JObject {
                                                                { "uuid", data.Properties["uuid"]?.ToString() },
                                                                { "response", true}
                                                            });
                            }
                            else if (data.Action.Equals("LoadProject", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "name" }))
                                {
                                    string _name = data.Properties["name"]?.ToString();
                                    session.SendXpressionResponse(category: "project",
                                                                 action: "LoadProject",
                                                                 value: new JObject {
                                                                    { "uuid", data.Properties["uuid"]?.ToString() },
                                                                    { "name", _name },
                                                                    { "response", xpn.LoadProject(name: _name)}
                                                                });
                                }
                            }
                            else if (data.Action.Equals("SaveProject", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "name" }))
                                {
                                    string _name = data.Properties["name"]?.ToString();
                                    session.SendXpressionResponse(category: "project",
                                                                 action: "SaveProject",
                                                                 value: new JObject {
                                                                    { "uuid", data.Properties["uuid"]?.ToString() },
                                                                    { "name", _name },
                                                                    { "response", xpn.SaveProject(name: _name)}
                                                                });
                                }
                            }

                            break;

                        case "takeitem":

                            if (data.Action.Equals("SetTakeItemOnline", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "takeID" }))
                                {
                                    // Make sure the takeID given is a valid integer
                                    if (int.TryParse(data.Properties["takeID"]?.ToString(), out int _takeID))
                                    {
                                        session.SendXpressionResponse(category: "takeitem",
                                                                     action: "SetTakeItemOnline",
                                                                     value: new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "takeID", _takeID },
                                                                        { "response", xpn.SetTakeItemOnline(takeID: _takeID)}
                                                                    });
                                    }
                                }
                            }
                            else if (data.Action.Equals("SetTakeItemOffline", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "takeID" }))
                                {
                                    // Make sure the takeID given is a valid integer
                                    if (int.TryParse(data.Properties["takeID"]?.ToString(), out int _takeID))
                                    {
                                        session.SendXpressionResponse(category: "takeitem",
                                                                     action: "SetTakeItemOffline",
                                                                     value: new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "takeID", _takeID },
                                                                        { "response", xpn.SetTakeItemOffline(takeID: _takeID)}
                                                                    });
                                    }
                                }
                            }
                            else if (data.Action.Equals("GetTakeItemStatus", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "takeID" }))
                                {
                                    // Make sure the takeID given is a valid integer
                                    if (int.TryParse(data.Properties["takeID"]?.ToString(), out int _takeID))
                                    {
                                        session.SendXpressionResponse(category: "takeitem",
                                                                     action: "GetTakeItemStatus",
                                                                     value: new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "takeID", _takeID },
                                                                        { "response", xpn.GetTakeItemStatus(takeID: _takeID)}
                                                                    });
                                    }
                                }
                            }
                            else if (data.Action.Equals("GetTakeItemLayer", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "takeID" }))
                                {
                                    // Make sure the takeID given is a valid integer
                                    if (int.TryParse(data.Properties["takeID"]?.ToString(), out int _takeID))
                                    {
                                        session.SendXpressionResponse(category: "takeitem",
                                                                     action: "GetTakeItemLayer",
                                                                     value: new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "takeID", _takeID },
                                                                        { "response", xpn.GetTakeItemLayer(takeID: _takeID)}
                                                                    });
                                    }
                                }
                            }
                            else if (data.Action.Equals("EditTakeItemProperty", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "takeID" }))
                                {
                                    // Make sure the takeID given is a valid integer
                                    if (int.TryParse(data.Properties["takeID"]?.ToString(), out int _takeID))
                                    {
                                        // Check different types of EditTakeItemProperty property options
                                        if (VerifyProperties(session: session,
                                                             serviceProps: data.Properties,
                                                             properties: new string[] { "objName", "propName", "value" },
                                                             invalidResponse: false))
                                        {
                                            session.SendXpressionResponse(category: "takeitem",
                                                                         action: "EditTakeItemProperty",
                                                                         value: new JObject {
                                                                             { "uuid", data.Properties["uuid"]?.ToString() },
                                                                             { "takeID", _takeID },
                                                                             { "response", xpn.EditTakeItemProperty(takeID: _takeID,
                                                                                                                    objName: data.Properties["objName"]?.ToString(),
                                                                                                                    propName: data.Properties["propName"]?.ToString(),
                                                                                                                    value: data.Properties["value"]?.ToString(),
                                                                                                                    materialName: data.Properties["materialName"]?.ToString())}
                                                                        });
                                        }
                                        else if (VerifyProperties(session: session,
                                                                  serviceProps: data.Properties,
                                                                  properties: new string[] { "objName", "propID", "value" },
                                                             invalidResponse: false))
                                        {
                                            if (int.TryParse(data.Properties["propID"]?.ToString(), out int _propID))
                                            {
                                                session.SendXpressionResponse(category: "project",
                                                                             action: "EditTakeItemProperty",
                                                                             value: new JObject {
                                                                                 { "uuid", data.Properties["uuid"]?.ToString() },
                                                                                 { "takeID", _takeID },
                                                                                 { "response", xpn.EditTakeItemProperty(takeID: _takeID,
                                                                                                                        objName: data.Properties["objName"]?.ToString(),
                                                                                                                        propID: _propID,
                                                                                                                        value: data.Properties["value"]?.ToString(),
                                                                                                                        materialName: data.Properties["materialName"]?.ToString())}
                                                                            });
                                            }
                                        }
                                        else if (VerifyProperties(session: session,
                                                                  serviceProps: data.Properties,
                                                                  properties: new string[] { "index", "propID", "value" },
                                                             invalidResponse: false))
                                        {
                                            if (int.TryParse(data.Properties["index"]?.ToString(), out int _index))
                                            {
                                                session.SendXpressionResponse(category: "takeitem",
                                                                             action: "EditTakeItemProperty",
                                                                             value: new JObject {
                                                                                { "uuid", data.Properties["uuid"]?.ToString() },
                                                                                { "takeID", _takeID },
                                                                                { "response", xpn.EditTakeItemProperty(takeID: _takeID,
                                                                                                                        index: _index,
                                                                                                                        propName: data.Properties["propName"]?.ToString(),
                                                                                                                        value: data.Properties["value"]?.ToString(),
                                                                                                                        materialName: data.Properties["materialName"]?.ToString())}
                                                                            });
                                            }
                                        }
                                        else if (VerifyProperties(session: session,
                                                                  serviceProps: data.Properties,
                                                                  properties: new string[] { "index", "propID", "value" },
                                                             invalidResponse: false))
                                        {
                                            if (int.TryParse(data.Properties["index"]?.ToString(), out int _index)
                                                && int.TryParse(data.Properties["propID"]?.ToString(), out int _propID))
                                            {
                                                session.SendXpressionResponse(category: "takeitem",
                                                                             action: "EditTakeItemProperty",
                                                                             value: new JObject {
                                                                                { "uuid", data.Properties["uuid"]?.ToString() },
                                                                                { "takeID", _takeID },
                                                                                { "response", xpn.EditTakeItemProperty(takeID: _takeID,
                                                                                                                       index: _index,
                                                                                                                       propID: _propID,
                                                                                                                       value: data.Properties["value"]?.ToString(),
                                                                                                                       materialName: data.Properties["materialName"]?.ToString())}
                                                                            });
                                            }
                                        }
                                        else
                                        {

                                            session.SendMessage(message: new JObject
                                                                {
                                                                    { "service", "xpression" },
                                                                    { "data", new JObject {
                                                                        { "category", "main" },
                                                                        { "action", "error" },
                                                                        { "value", new JObject {
                                                                            { "uuid", data.Properties["uuid"]?.ToString() },
                                                                            { "response", "Invalid properties!"}
                                                                        }},
                                                                    }}
                                                                });
                                        }
                                    }
                                }
                            }

                            break;

                        case "scene":

                            if (data.Action.Equals("TakeScene", StringComparison.OrdinalIgnoreCase))
                            {
                                // Check different types of TakeScene property options
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "sceneName" },
                                                     invalidResponse: false))
                                {
                                    string _sceneName = data.Properties["sceneName"]?.ToString();
                                    session.SendXpressionResponse(category: "scene",
                                                                 action: "TakeScene",
                                                                 value: new JObject {
                                                                    { "uuid", data.Properties["uuid"]?.ToString() },
                                                                    { "sceneName", _sceneName },
                                                                    { "response", xpn.TakeScene(sceneName: _sceneName,
                                                                                                online: (bool.TryParse(data.Properties["online"]?.ToString(), result: out bool _online) ? _online : true),
                                                                                                layerIndex: (int.TryParse(data.Properties["layerIndex"]?.ToString(), result: out int _layerIndex) ? _layerIndex : 0),
                                                                                                frameBuffer: (int.TryParse(data.Properties["frameBuffer"]?.ToString(), out int _frameBuffer) ? _frameBuffer : 0))}
                                                                });
                                }
                                else if (VerifyProperties(session: session,
                                                          serviceProps: data.Properties,
                                                          properties: new string[] { "uuid", "sceneID" },
                                                          invalidResponse: false))
                                {
                                    if (int.TryParse(data.Properties["sceneID"]?.ToString(), out int _sceneID))
                                    {
                                        session.SendXpressionResponse(category: "scene",
                                                                     action: "TakeScene",
                                                                     value: new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "sceneID", _sceneID },
                                                                        { "response", xpn.TakeScene(sceneID: _sceneID,
                                                                                                    online: (bool.TryParse(data.Properties["online"]?.ToString(), result: out bool _online) ? _online : true),
                                                                                                    layerIndex: (int.TryParse(data.Properties["layerIndex"]?.ToString(), result: out int _layerIndex) ? _layerIndex : 0),
                                                                                                    frameBuffer: (int.TryParse(data.Properties["frameBuffer"]?.ToString(), out int _frameBuffer) ? _frameBuffer : 0))}
                                                                    });
                                    }
                                }
                                else
                                {

                                    session.SendMessage(message: new JObject
                                                            {
                                                                { "service", "xpression" },
                                                                { "data", new JObject {
                                                                    { "category", "main" },
                                                                    { "action", "error" },
                                                                    { "value", new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "response", "Invalid properties!"}
                                                                    }},
                                                                }}
                                                            });
                                }
                            }
                            else if (data.Action.Equals("PlaySceneDirector", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "takeID", "directorName" },
                                                     invalidResponse: false))
                                {
                                    // Make sure the takeID given is a valid integer
                                    if (int.TryParse(data.Properties["takeID"]?.ToString(), out int _takeID))
                                    {
                                        session.SendXpressionResponse(category: "scene",
                                                                     action: "PlaySceneDirector",
                                                                     value: new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "takeID", _takeID },
                                                                        { "response", xpn.PlaySceneDirector(takeID: _takeID,
                                                                                                            directorName: data.Properties["directorName"]?.ToString())}
                                                                    });
                                    }
                                }
                            }
                            else if (data.Action.Equals("GetSceneDirectorName", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "takeID" },
                                                     invalidResponse: false))
                                {
                                    // Make sure the takeID given is a valid integer
                                    if (int.TryParse(data.Properties["takeID"]?.ToString(), out int _takeID))
                                    {
                                        session.SendXpressionResponse(category: "scene",
                                                                     action: "GetSceneDirectorName",
                                                                     value: new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "takeID", _takeID },
                                                                        { "response", xpn.GetSceneDirectorName(takeID: _takeID)}
                                                                    });
                                    }
                                }
                            }
                            else if (data.Action.Equals("SetSceneDirector", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "takeID", "directorName" }))
                                {
                                    // Make sure the takeID given is a valid integer
                                    if (int.TryParse(data.Properties["takeID"]?.ToString(), out int _takeID))
                                    {
                                        session.SendXpressionResponse(category: "scene",
                                                                     action: "SetSceneDirector",
                                                                     value: new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "takeID", _takeID },
                                                                        { "response", xpn.SetSceneDirector(takeID: _takeID,
                                                                                                           directorName: data.Properties["directorName"]?.ToString())}
                                                                    });
                                    }
                                }
                            }
                            else if (data.Action.Equals("AnimateScene", StringComparison.OrdinalIgnoreCase))
                            {
                                // Check different types of TakeScene property options
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "sceneName", "animName" },
                                                     invalidResponse: false))
                                {
                                    string _sceneName = data.Properties["sceneName"]?.ToString();
                                    session.SendXpressionResponse(category: "scene",
                                                                 action: "AnimateScene",
                                                                 value: new JObject {
                                                                    { "uuid", data.Properties["uuid"]?.ToString() },
                                                                    { "sceneName", _sceneName },
                                                                    { "response", xpn.AnimateScene(sceneName: _sceneName,
                                                                                                   animName: data.Properties["animName"]?.ToString(),
                                                                                                   waitFor: (bool.TryParse(data.Properties["waitFor"]?.ToString(), out bool _waitFor) ? _waitFor : false),
                                                                                                   timeOut: (int.TryParse(data.Properties["timeOut"]?.ToString(), out int _timeOut) ? _timeOut : -1))}
                                                                });
                                }
                                else if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "sceneID", "animName" },
                                                     invalidResponse: false))
                                {
                                    if (int.TryParse(data.Properties["sceneID"]?.ToString(), out int _sceneID))
                                    {
                                        session.SendXpressionResponse(category: "scene",
                                                                     action: "AnimateScene",
                                                                     value: new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "sceneID", _sceneID },
                                                                        { "response", xpn.AnimateScene(sceneID: _sceneID,
                                                                                                       animName: data.Properties["animName"]?.ToString(),
                                                                                                       waitFor: (bool.TryParse(data.Properties["waitFor"]?.ToString(), out bool _waitFor) ? _waitFor : false),
                                                                                                       timeOut: (int.TryParse(data.Properties["timeOut"]?.ToString(), out int _timeOut) ? _timeOut : -1))}
                                                                    });
                                    }
                                }
                                else if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "sceneName", "animID" },
                                                     invalidResponse: false))
                                {
                                    if (int.TryParse(data.Properties["animID"]?.ToString(), out int _animID))
                                    {
                                        string _sceneName = data.Properties["sceneName"]?.ToString();
                                        session.SendXpressionResponse(category: "scene",
                                                                     action: "AnimateScene",
                                                                     value: new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "sceneName", _sceneName },
                                                                        { "response", xpn.AnimateScene(sceneName: _sceneName,
                                                                                                    animID: _animID,
                                                                                                    waitFor: (bool.TryParse(data.Properties["waitFor"]?.ToString(), out bool _waitFor) ? _waitFor : false),
                                                                                                    timeOut: (int.TryParse(data.Properties["timeOut"]?.ToString(), out int _timeOut) ? _timeOut : -1))}
                                                                    });
                                    }
                                }
                                else if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "sceneID", "animID" },
                                                     invalidResponse: false))
                                {
                                    if (int.TryParse(data.Properties["sceneID"]?.ToString(), out int _sceneID)
                                        && int.TryParse(data.Properties["animID"]?.ToString(), out int _animID))
                                    {
                                        session.SendXpressionResponse(category: "scene",
                                                                     action: "AnimateScene",
                                                                     value: new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "sceneID", _sceneID },
                                                                        { "response", xpn.AnimateScene(sceneID: _sceneID,
                                                                                                       animID: _animID,
                                                                                                       waitFor: (bool.TryParse(data.Properties["waitFor"]?.ToString(), out bool _waitFor) ? _waitFor : false),
                                                                                                       timeOut: (int.TryParse(data.Properties["timeOut"]?.ToString(), out int _timeOut) ? _timeOut : -1))}
                                                                    });
                                    }
                                }
                                else
                                {

                                    session.SendMessage(message: new JObject
                                                            {
                                                                { "service", "xpression" },
                                                                { "data", new JObject {
                                                                    { "category", "main" },
                                                                    { "action", "error" },
                                                                    { "value", new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "response", "Invalid properties!"}
                                                                    }},
                                                                }}
                                                            });
                                }
                            }

                            break;

                        case "editscene":

                            if (data.Action.Equals("EditSceneText", StringComparison.OrdinalIgnoreCase))
                            {
                                // Check different types of TakeScene property options
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "sceneName", "txtName", "value" },
                                                     invalidResponse: false))
                                {
                                    string _sceneName = data.Properties["sceneName"]?.ToString();
                                    session.SendXpressionResponse(category: "editscene",
                                                                 action: "EditSceneText",
                                                                 value: new JObject {
                                                                    { "uuid", data.Properties["uuid"]?.ToString() },
                                                                    { "sceneName", _sceneName },
                                                                    { "response", xpn.EditSceneText(sceneName: _sceneName,
                                                                                                    txtName: data.Properties["txtName"]?.ToString(),
                                                                                                    value: data.Properties["value"]?.ToString())}
                                                                });
                                }
                                else if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "sceneID", "txtName", "value" },
                                                     invalidResponse: false))
                                {
                                    if (int.TryParse(data.Properties["sceneID"]?.ToString(), out int _sceneID))
                                    {
                                        session.SendXpressionResponse(category: "editscene",
                                                                     action: "EditSceneText",
                                                                     value: new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "sceneID", _sceneID },
                                                                        { "response", xpn.EditSceneText(sceneID: _sceneID,
                                                                                                        txtName: data.Properties["txtName"]?.ToString(),
                                                                                                        value: data.Properties["value"]?.ToString())}
                                                                    });
                                    }
                                }
                                else
                                {

                                    session.SendMessage(message: new JObject
                                                        {
                                                            { "service", "xpression" },
                                                            { "data", new JObject {
                                                                { "category", "main" },
                                                                { "action", "error" },
                                                                { "value", new JObject {
                                                                    { "uuid", data.Properties["uuid"]?.ToString() },
                                                                    { "response", "Invalid properties!"}
                                                                }},
                                                            }}
                                                        });
                                }
                            }

                            break;

                        case "widget":

                            if (data.Action.Equals("EditCounterWidget", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "name", "value" }))
                                {
                                    if (int.TryParse(data.Properties["value"]?.ToString(), out int _value))
                                    {
                                        string _name = data.Properties["name"]?.ToString();
                                        int _result = xpn.EditCounterWidget(name: _name,
                                                                                value: _value);
                                        if (_result != Globals.INVALID_INT)
                                            session.SendXpressionResponse(category: "widget",
                                                                        action: "EditCounterWidget",
                                                                        value: new JObject {
                                                                            { "uuid", data.Properties["uuid"]?.ToString() },
                                                                            { "name", _name },
                                                                            { "response", _result}
                                                                        });
                                        else
                                            session.SendXpressionResponse(category: "main",
                                                                         action: "error",
                                                                         value: new JObject {
                                                                            { "uuid", data.Properties["uuid"]?.ToString() },
                                                                            { "name", _name },
                                                                            { "response", $"Failed to EditCounterWidget: {_name}"}
                                                                        });
                                    }
                                }
                            }
                            else if (data.Action.Equals("IncreaseCounterWidget", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "name", "value" }))
                                {
                                    if (int.TryParse(data.Properties["value"]?.ToString(), out int _value))
                                    {
                                        string _name = data.Properties["name"]?.ToString();
                                        int _result = xpn.IncreaseCounterWidget(name: _name,
                                                                                value: _value);
                                        if (_result != Globals.INVALID_INT)
                                            session.SendXpressionResponse(category: "widget",
                                                                         action: "IncreaseCounterWidget",
                                                                         value: new JObject {
                                                                            { "uuid", data.Properties["uuid"]?.ToString() },
                                                                            { "name", _name },
                                                                            { "response", _result}
                                                                        });
                                        else
                                            session.SendXpressionResponse(category: "main",
                                                                         action: "error",
                                                                         value: new JObject {
                                                                            { "uuid", data.Properties["uuid"]?.ToString() },
                                                                            { "name", _name },
                                                                            { "response", $"Failed to IncreaseCounterWidget: {_name}"}
                                                                        });
                                    }
                                }
                            }
                            else if (data.Action.Equals("DecreaseCounterWidget", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "name", "value" }))
                                {
                                    if (int.TryParse(data.Properties["value"]?.ToString(), out int _value))
                                    {
                                        string _name = data.Properties["name"]?.ToString();
                                        int _result = xpn.DecreaseCounterWidget(name: _name,
                                                                                value: _value);
                                        if (_result != Globals.INVALID_INT)
                                            session.SendXpressionResponse(category: "widget",
                                                                         action: "DecreaseCounterWidget",
                                                                         value: new JObject {
                                                                            { "uuid", data.Properties["uuid"]?.ToString() },
                                                                            { "name", _name },
                                                                            { "response", _result}
                                                                        });
                                        else
                                            session.SendXpressionResponse(category: "main",
                                                                         action: "error",
                                                                         value: new JObject {
                                                                            { "uuid", data.Properties["uuid"]?.ToString() },
                                                                            { "name", _name },
                                                                            { "response", $"Failed to DecreaseCounterWidget: {_name}"}
                                                                        });
                                    }
                                }
                            }
                            else if (data.Action.Equals("GetCounterWidgetValue", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "name" }))
                                {
                                    string _name = data.Properties["name"]?.ToString();
                                    int _result = xpn.GetCounterWidgetValue(name: _name);
                                    if (_result != Globals.INVALID_INT)
                                        session.SendXpressionResponse(category: "widget",
                                                                    action: "GetCounterWidgetValue",
                                                                    value: new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "name", _name },
                                                                        { "response", _result}
                                                                    });
                                    else
                                        session.SendXpressionResponse(category: "main",
                                                                     action: "error",
                                                                     value: $"Failed to GetCounterWidgetValue: {_name}");
                                }
                            }
                            else if (data.Action.Equals("SetTextListWidgetItemIndex", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "name", "index" }))
                                {
                                    if (int.TryParse(data.Properties["index"]?.ToString(), out int _index))
                                    {
                                        string _name = data.Properties["name"]?.ToString();
                                        session.SendXpressionResponse(category: "widget",
                                                                     action: "SetTextListWidgetItemIndex",
                                                                     value: new JObject {
                                                                            { "uuid", data.Properties["uuid"]?.ToString() },
                                                                            { "name", _name },
                                                                            { "response", xpn.SetTextListWidgetItemIndex(name: _name,
                                                                                                                        index: _index)}
                                                                    });
                                    }
                                }
                            }
                            else if (data.Action.Equals("TextListWidgetNext", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "name" }))
                                {
                                    string _name = data.Properties["name"]?.ToString();
                                    session.SendXpressionResponse(category: "widget",
                                                                 action: "TextListWidgetNext",
                                                                 value: new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "name", _name },
                                                                        { "response", xpn.TextListWidgetNext(name: _name)}
                                                                });
                                }
                            }
                            else if (data.Action.Equals("TextListWidgetPrev", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "name" }))
                                {
                                    string _name = data.Properties["name"]?.ToString();
                                    session.SendXpressionResponse(category: "widget",
                                                                 action: "TextListWidgetPrev",
                                                                 value: new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "name", _name },
                                                                        { "response", xpn.TextListWidgetPrev(name: _name)}
                                                                });
                                }
                            }
                            else if (data.Action.Equals("ResetTextListWidgetIndex", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "name" }))
                                {
                                    string _name = data.Properties["name"]?.ToString();
                                    session.SendXpressionResponse(category: "widget",
                                                                 action: "ResetTextListWidgetIndex",
                                                                 value: new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "name", _name },
                                                                        { "response", xpn.ResetTextListWidgetIndex(name: _name)}
                                                                });
                                }
                            }
                            else if (data.Action.Equals("AddTextListWidgeString", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "name", "value" }))
                                {
                                    string _name = data.Properties["name"]?.ToString();
                                    session.SendXpressionResponse(category: "widget",
                                                                 action: "AddTextListWidgeString",
                                                                 value: new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "name", _name },
                                                                        { "response", xpn.AddTextListWidgeString(name: _name,
                                                                                                                 value: data.Properties["value"]?.ToString())}
                                                                });
                                }
                            }
                            else if (data.Action.Equals("SetTextListWidgetValues", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "name", "values" }))
                                {
                                    string _name = data.Properties["name"]?.ToString();
                                    session.SendXpressionResponse(category: "widget",
                                                                 action: "SetTextListWidgetValues",
                                                                 value: new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "name", _name },
                                                                        { "response", xpn.SetTextListWidgetValues(name: _name,
                                                                                                                  values: data.Properties["values"]?.ToString().Split(';'))}
                                                                });
                                }
                            }
                            else if (data.Action.Equals("SetTextListWidgetToValue", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "name", "value" }))
                                {
                                    string _name = data.Properties["name"]?.ToString();
                                    session.SendXpressionResponse(category: "widget",
                                                                 action: "SetTextListWidgetToValue",
                                                                 value: new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "name", _name },
                                                                        { "response", xpn.SetTextListWidgetToValue(name: _name,
                                                                                                                   value: data.Properties["value"]?.ToString())}
                                                                });
                                }
                            }
                            else if (data.Action.Equals("ClearTextListWidget", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "name" }))
                                {
                                    string _name = data.Properties["name"]?.ToString();
                                    session.SendXpressionResponse(category: "widget",
                                                                 action: "ClearTextListWidget",
                                                                 value: new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "name", _name },
                                                                        { "response", xpn.ClearTextListWidget(name: _name)}
                                                                });
                                }
                            }
                            else if (data.Action.Equals("GetTextListWidgetValue", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "name" }))
                                {
                                    string _name = data.Properties["name"]?.ToString();
                                    session.SendXpressionResponse(category: "widget",
                                                                 action: "GetTextListWidgetValue",
                                                                 value: new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "name", _name },
                                                                        { "response", xpn.GetTextListWidgetValue(name: _name)}
                                                                });
                                }
                            }
                            else if (data.Action.Equals("GetTextListWidgetValues", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "name" }))
                                {
                                    string _name = data.Properties["name"]?.ToString();
                                    session.SendXpressionResponse(category: "widget",
                                                                 action: "GetTextListWidgetValues",
                                                                 value: new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "name", _name },
                                                                        { "response", string.Join(";", xpn.GetTextListWidgetValues(name: _name))}
                                                                });
                                }
                            }
                            else if (data.Action.Equals("GetTextListWidgetItemIndex", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "name" }))
                                {
                                    string _name = data.Properties["name"]?.ToString();
                                    session.SendXpressionResponse(category: "widget",
                                                                 action: "GetTextListWidgetItemIndex",
                                                                 value: new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "name", _name },
                                                                        { "response", xpn.GetTextListWidgetItemIndex(name: _name)}
                                                                });
                                }
                            }
                            else if (data.Action.Equals("GetTextListWidgetCount", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "name" }))
                                {
                                    string _name = data.Properties["name"]?.ToString();
                                    session.SendXpressionResponse(category: "widget",
                                                                 action: "GetTextListWidgetCount",
                                                                 value: new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "name", _name },
                                                                        { "response", xpn.GetTextListWidgetCount(name: _name)}
                                                                });
                                }
                            }
                            else if (data.Action.Equals("GetClockWidgetTimerValue", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "name" }))
                                {
                                    string _name = data.Properties["name"]?.ToString();
                                    session.SendXpressionResponse(category: "widget",
                                                                 action: "GetClockWidgetTimerValue",
                                                                 value: new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "name", _name },
                                                                        { "response", xpn.GetClockWidgetTimerValue(name: _name)}
                                                                });
                                }
                            }
                            else if (data.Action.Equals("GetClockWidgetValue", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "name" }))
                                {
                                    string _name = data.Properties["name"]?.ToString();
                                    session.SendXpressionResponse(category: "widget",
                                                                 action: "GetClockWidgetValue",
                                                                 value: new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "name", _name },
                                                                        { "response", xpn.GetClockWidgetValue(name: _name)}
                                                                });
                                }
                            }
                            else if (data.Action.Equals("StartClockWidget", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "name" }))
                                {
                                    string _name = data.Properties["name"]?.ToString();
                                    session.SendXpressionResponse(category: "widget",
                                                                 action: "StartClockWidget",
                                                                 value: new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "name", _name },
                                                                        { "response", xpn.StartClockWidget(name: _name)}
                                                                });
                                }
                            }
                            else if (data.Action.Equals("StopClockWidget", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "name" }))
                                {
                                    string _name = data.Properties["name"]?.ToString();
                                    session.SendXpressionResponse(category: "widget",
                                                                 action: "StopClockWidget",
                                                                 value: new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "name", _name },
                                                                        { "response", xpn.StopClockWidget(name: _name)}
                                                                });
                                }
                            }
                            else if (data.Action.Equals("ResetClockWidget", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "name" }))
                                {
                                    string _name = data.Properties["name"]?.ToString();
                                    session.SendXpressionResponse(category: "widget",
                                                                 action: "ResetClockWidget",
                                                                 value: new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "name", _name },
                                                                        { "response", xpn.ResetClockWidget(name: _name)}
                                                                });
                                }
                            }
                            else if (data.Action.Equals("EditClockWidgetFormat", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "name", "format" }))
                                {
                                    string _name = data.Properties["name"]?.ToString();
                                    string _format = data.Properties["format"]?.ToString();
                                    if (string.IsNullOrEmpty(_format))
                                        _format = "NN:SS";

                                    session.SendXpressionResponse(category: "widget",
                                                                 action: "EditClockWidgetFormat",
                                                                 value: new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "name", _name },
                                                                        { "response", xpn.EditClockWidgetFormat(name: _name,
                                                                                                                format: _format)}
                                                                });
                                }
                            }
                            else if (data.Action.Equals("EditClockWidgetStartTime", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "name", "startTime" }))
                                {
                                    string _name = data.Properties["name"]?.ToString();
                                    session.SendXpressionResponse(category: "widget",
                                                                 action: "EditClockWidgetStartTime",
                                                                 value: new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "name", _name },
                                                                        { "response", xpn.EditClockWidgetStartTime(name: _name,
                                                                                                                   startTime: data.Properties["startTime"]?.ToString())}
                                                                });
                                }
                            }
                            else if (data.Action.Equals("SetClockWidgetTimerValue", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "name", "value" }))
                                {
                                    if (int.TryParse(data.Properties["value"]?.ToString(), out int _value))
                                    {
                                        string _name = data.Properties["name"]?.ToString();
                                        session.SendXpressionResponse(category: "widget",
                                                                     action: "SetClockWidgetTimerValue",
                                                                     value: new JObject {
                                                                            { "uuid", data.Properties["uuid"]?.ToString() },
                                                                            { "name", _name },
                                                                            { "response", xpn.SetClockWidgetTimerValue(name: _name,
                                                                                                                    value: _value)}
                                                                    });
                                    }
                                }
                            }
                            else if (data.Action.Equals("SetClockWidgetCallback", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "name", "callback" }))
                                {
                                    string _name = data.Properties["name"]?.ToString();
                                    session.SendXpressionResponse(category: "widget",
                                                                 action: "SetClockWidgetCallback",
                                                                 value: new JObject {
                                                                        { "uuid", data.Properties["uuid"]?.ToString() },
                                                                        { "name", _name },
                                                                        { "response", session.SetClockWidgetCallback(xpn: xpn,
                                                                                                                    name: _name,
                                                                                                                    callback: data.Properties["callback"]?.ToString())}
                                                                });
                                }
                            }

                            break;

                        case "metadata":

                            if (data.Action.Equals("EditMetadataAttribute", StringComparison.OrdinalIgnoreCase))
                            {
                                // Make sure the message has all required properties for this action
                                if (VerifyProperties(session: session,
                                                     serviceProps: data.Properties,
                                                     properties: new string[] { "uuid", "sceneID", "attrName", "value" }))
                                {
                                    // Make sure the takeID given is a valid integer
                                    if (int.TryParse(data.Properties["sceneID"]?.ToString(), out int _sceneID))
                                    {
                                        session.SendXpressionResponse(category: "metadata",
                                                                    action: "EditMetadataAttribute",
                                                                    value: new JObject {
                                                                            { "uuid", data.Properties["uuid"]?.ToString() },
                                                                            { "sceneID", _sceneID },
                                                                            { "response", xpn.EditMetadataAttribute(sceneID: _sceneID,
                                                                                                                    attrName: data.Properties["attrName"]?.ToString(),
                                                                                                                    value: data.Properties["value"]?.ToString())}
                                                                    });
                                    }
                                }
                            }

                            break;
                    }
                }
                else
                {
                    parent.AddMessage(JObject.FromObject(data), $"Error: Invalid Xpression request", "Incoming");
                    session.SendMessage(message: new JObject
                        {
                            { "service", "xpression" },
                            { "data", new JObject {
                                { "category", "main" },
                                { "action", "error" },
                                { "value", new JObject {
                                    { "uuid", data.Properties["uuid"]?.ToString() },
                                    { "response", "Category or Action missing!"}
                                }},
                            }}
                        });
                }
            }
            catch (Exception e)
            {
                parent.AddMessage(JObject.FromObject(e), $"Error: {e.Message}", "Incoming");
                session.SendMessage(message: new JObject
                        {
                            { "service", "xpression" },
                            { "data", new JObject {
                                { "category", "main" },
                                { "action", "error" },
                                { "value", new JObject {
                                    { "uuid", data.Properties["uuid"]?.ToString() },
                                    { "response", e.Message }
                                }},
                            }}
                        });
            };
        }
    }
}
