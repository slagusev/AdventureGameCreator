using Editor.ObjectTypes;
using Editor.Scripter;
using Editor.Scripter.Conditions;
using Editor.Scripter.Flow;
using Editor.Scripter.ItemManagement;
using Editor.Scripter.Misc;
using Editor.Scripter.TextFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Editor
{
    class TestData
    {
        public static Script TestScript { get; set; }
        public static Room TestRoom { get; set; }
        public static Room TestScriptRoom { get; set; }
        
        //Test script types
        public static Comment TestComment { get; set; }
        public static DisplayText TestDisplayText { get; set; }
        public static AddText TestAddText { get; set; }
        public static Conditional TestCondition1 { get; set; }
        public static Conditional TestConditionNested { get; set; }
        public static Conditional TestConditionChild { get; set; }
        public static SetVariable TestSetVariable { get; set; }
        public static AddItemToInventory TestAddItem { get; set; }
        public static GetItemProperty TestGetProperty { get; set; }
        public static SetItemProperty TestSetProperty { get; set; }
        //Interactable samples
        public static Interactable TestInteractable { get; set; }
        public static InteractableGroup TestInteractableGroup { get; set; }

        //Variable Samples
        public static Variable TestStringVariable { get; set; }
        public static Variable TestDateTimeVariable { get; set; }
        public static Variable TestNumberVariable { get; set; }
        public static ItemClass TestItemClass { get; set; }
        public static Item TestItem { get; set; }

        static TestData()
        {

            //MainViewModel.MainViewModelStatic.ItemClasses.Add(Editor.ObjectTypes.ItemClass.GetBaseItemClass());
            TestScript = new Script();
            TestScript.ScriptLines.Clear();
            TestScript.ScriptLines.Add(new Comment { CommentText = "test comment" });
            TestScript.ScriptLines.Add(new Comment { CommentText = "another test comment" });
            TestCondition1 = new Conditional(TestScript);
            TestCondition1.ThenStatement.AddBeforeSelected(new Comment { CommentText = "Test Conditional Comment" });
            TestCondition1.ElseStatement.AddBeforeSelected(new Comment { CommentText = "Test Else Comment" });
            TestConditionNested = new Conditional(TestScript);
            TestConditionChild = new Conditional(TestScript);
            TestConditionChild.ThenStatement.AddBeforeSelected(new Comment { CommentText = "Another comment" });
            TestConditionChild.ElseStatement.AddBeforeSelected(new Comment { CommentText = "Another else comment" });
            TestConditionNested.ThenStatement.AddBeforeSelected(TestConditionChild);
            TestConditionChild = new Conditional(TestScript);
            TestScript.ScriptLines.Add(TestCondition1);
            TestScript.ScriptLines.Add(TestConditionNested);
            TestScript.ScriptLines.Add(new Blank());
            TestComment = new Comment { CommentText = "This is my comment." };
            TestDisplayText = new DisplayText { Text = "The room is dark and reeks of old milk.\n\nThere is an unlit chandalier." };
            TestAddText = new AddText { Text = "A fresh, crisp, tasty looking apple." };
            TestRoom = new Room();
            TestRoom.RoomName = "Test Room";
            TestRoom.Description = "Test Room Description";
            TestRoom.HasPlaintextDescription = false;
            TestRoom.RoomID = new Guid("{1CF2C7E6-276C-4678-951C-2D1B1F239620}");
            TestRoom.RoomDescriptionScript = TestScript;

            TestScriptRoom = new Room();
            TestScriptRoom.RoomName = "Test Room";
            TestScriptRoom.Description = "Test Room Description";
            TestScriptRoom.HasPlaintextDescription = true;
            TestScriptRoom.RoomID = new Guid("{1CF2C7E6-276C-4678-951C-2D1B1F239620}");
            TestScriptRoom.RoomDescriptionScript = TestScript;

            TestInteractable = new Interactable();
            
            TestInteractable.InteractableName = "Bookshelf";
            TestInteractable.DefaultDisplayName = "Old Bookshelf";
            TestInteractable.CanExamine = true;
            TestInteractable.CanExamineUsesScript = true;
            TestInteractable.CanInteract = true;
            TestInteractable.CanInteractUsesScript = false;
            Script testIntCanExScript = new Script();
            testIntCanExScript.AddBeforeSelected(new Comment { CommentText = "Test Can Examine Script" });
            Script testIntCanInScript = new Script();
            testIntCanInScript.AddBeforeSelected(new Comment { CommentText = "Test Can Interact Script" });
            Script testIntExScript = new Script();
            testIntExScript.AddBeforeSelected(new Comment { CommentText = "Test Examine Script" });
            Script testIntInScript = new Script();
            testIntInScript.AddBeforeSelected(new Comment { CommentText = "Test Interaction Script" });
            TestInteractable.ExamineScript = testIntExScript;
            TestInteractable.InteractScript = testIntInScript;
            TestInteractable.CanInteractScript = testIntCanInScript;
            TestInteractable.CanExamineScript = testIntCanExScript;
            TestInteractable.GroupName = "Furniture";

            TestInteractableGroup = MainViewModel.MainViewModelStatic.InteractableGroups.FirstOrDefault();

            TestRoom.DefaultInteractables.Add(new InteractableRef(TestInteractable.InteractableID));

            TestDateTimeVariable = new Variable();
            TestDateTimeVariable.IsDateTime = true;
            TestDateTimeVariable.DefaultDateTime = new DateTime(2015, 1, 30, 10, 20, 5);
            TestDateTimeVariable.Name = "Test Date Time";

            TestStringVariable = new Variable();
            TestStringVariable.IsString = true;
            TestStringVariable.DefaultString = "Test Data";
            TestStringVariable.Name = "Test String";

            TestNumberVariable = new Variable();
            TestNumberVariable.IsNumber = true;
            TestNumberVariable.DefaultNumber = 5;
            TestNumberVariable.Name = "Test Number";

            TestSetVariable = new SetVariable();
            TestSetVariable.SelectedVariable = new VarRef(TestNumberVariable.Id);
            //var testParent = new ItemClass() { Name = "Item" };
            //MainViewModel.MainViewModelStatic.ItemClasses.Add(TestItemClass);
            TestItemClass = new ItemClass { Name = "Clothing", ParentClass = ItemClass.GetBaseItemClass() };
            TestItemClass.ItemProperties.Add(new Variable { Name = "Slot", IsString = true, DefaultString = "Body" });
            TestItemClass.ItemProperties.Add(new Variable { Name = "Weight", IsNumber = true, DefaultNumber = 40 });
            //MainViewModel.MainViewModelStatic.ItemClasses.Add(TestItemClass);
            var testChild1 = new ItemClass() { Name = "Shirts", ParentClass=TestItemClass };
            var testChild2 = new ItemClass() { Name = "Pants", ParentClass = TestItemClass };
            var testChild3 = new ItemClass() { Name = "Underwear", ParentClass = TestItemClass };
            var testChild4 = new ItemClass() { Name = "Food", ParentClass = TestItemClass };
            testChild4.ParentClass = TestItemClass.ParentClass;
            var testItem = new Item() { ItemClassParent = TestItemClass, DefaultName = "Helmet" };
            
            var testItem2 = new Item() { ItemClassParent = TestItemClass, DefaultName = "Bracers" };
            var testItem3 = new Item() { ItemClassParent = TestItemClass, DefaultName = "Apple" };
            testItem3.ItemClassParent = TestItemClass.ParentClass;

            TestItemClass.SelectedProperty = TestItemClass.ItemProperties.First();
            testItem2.ItemName = "BasicBracers";
            testItem2.SelectedProperty = testItem.ItemProperties.First();
            TestItem = testItem2;

            TestAddItem = new AddItemToInventory { ItemReference = new ItemRef(testItem.ItemID) };
            
            TestGetProperty = new GetItemProperty { VarRef = new VarRef(TestStringVariable.Id), SelectedItemClass = TestItemClass };
            TestGetProperty.SelectedProperty = TestGetProperty.SelectedItemClass.ItemProperties.First();
            TestSetProperty = new SetItemProperty { VarRef = new VarRef(TestStringVariable.Id), SelectedItemClass = TestItemClass };
            TestSetProperty.SelectedProperty = TestSetProperty.SelectedItemClass.ItemProperties.First();
        }
    }
}
