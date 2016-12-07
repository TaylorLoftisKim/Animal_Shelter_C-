using Nancy;
using System;
using System.Collections.Generic;
using AnimalShelter.Objects;

namespace AnimalShelter
{
  public class HomeModule: NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };
      Get["/animals/new"] = _ => {
        return View["animal_form.cshtml"];
      };
      Post["/animals/confirm"] = _ => {
        string animalName = Request.Form["animal-name"];
        string animalGender = Request.Form["animal-gender"];
        DateTime animalAdmittance = Request.Form["animal-admittance"];
        string animalBreed = Request.Form["animal-breed"];

        Animal newAnimal = new Animal(animalName, animalGender, animalAdmittance, animalBreed);
        newAnimal.Save();
        return View["animal_confirmation.cshtml", newAnimal];
      };
      Get["/animals"] = _ => {
        List<Animal> allAnimals = Animal.GetAll();
        return View["animals.cshtml", allAnimals];
      };
      Get["/animals/delete"] = _ => {
        Animal.DeleteAll();
        return View["animal_deleted.cshtml"];
      };
      Get["/types/new"] = _ => {
        return View["type_form.cshtml"];
      };
      Post["/types/confirm"] = _ => {
        return View["type_confirmation.cshtml"];
      };
      Get["/types"] = _ => {
        List<AnimalType> types = AnimalType.GetAll();
        return View["types.cshtml", types];
      };
      Get["/types/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        AnimalType selectedAnimalType = AnimalType.Find(parameters.id);
        List<Animal> selectedAnimals = selectedAnimalType.GetAnimals();
        model.Add("animal type", selectedAnimalType);
        model.Add("animals", selectedAnimals);
        return ["type.cshtml", model];
      };
    }
  }
}
